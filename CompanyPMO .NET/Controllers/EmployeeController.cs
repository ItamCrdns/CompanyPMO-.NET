﻿using CompanyPMO_.NET.Dto;
using CompanyPMO_.NET.Interfaces;
using CompanyPMO_.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPMO_.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        private readonly IJwt _jwtService;

        public EmployeeController(IEmployee employeeService, IJwt jwtService)
        {
            _employeeService = employeeService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        public async Task<IActionResult> Login([FromForm] EmployeeLoginDto employee)
        {
            var employeeAuthentication = await _employeeService.AuthenticateEmployee(employee.Username, employee.Password);

            if(employeeAuthentication.result.Authenticated)
            {
                var loggedEmployee = await _employeeService.GetEmployeeForClaims(employee.Username);

                var token = _jwtService.JwtTokenGenerator(loggedEmployee);

                HttpContext.Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true, // No HTTPS
                    IsEssential = true
                });

                var loginResponse = new LoginResponseDto
                {
                    Result = employeeAuthentication.result,
                    Message = employeeAuthentication.message,
                    Employee = employeeAuthentication.employee
                };

                return Ok(loginResponse);
            }
            else if(!employeeAuthentication.result.Authenticated)
            {
                var loginResponse = new LoginResponseDto
                {
                    Result = employeeAuthentication.result,
                    Message = employeeAuthentication.message,
                    Employee = employeeAuthentication.employee
                };

                return Unauthorized(loginResponse);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RegisterEmployee([FromForm] EmployeeRegisterDto employee, [FromForm] IFormFile? profilePicture)
        {
            bool newEmployee = await _employeeService.RegisterEmployee(employee, profilePicture);

            return Ok(new { Created = newEmployee });
        }

        [Authorize(Policy = "SupervisorOnly")]
        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            Employee employee = await _employeeService.GetEmployeeById(employeeId);

            return Ok(employee);
        }
    }
}
