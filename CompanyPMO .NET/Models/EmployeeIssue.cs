﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPMO_.NET.Models
{
    public class EmployeeIssue
    {
        [Column("relation_id")]
        public int RelationId { get; set; }
        [Column("employee_id")]
        public int EmployeeId { get; set; }
        [Column("issue_id")]
        public int IssueId { get; set; }
    }
}