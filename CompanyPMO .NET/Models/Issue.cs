﻿using CompanyPMO_.NET.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPMO_.NET.Models
{
    [Table("issues")]
    public class Issue
    {
        [Column("issue_id")]
        public int IssueId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("started_working")]
        public DateTime? StartedWorking { get; set; }
        [Column("fixed")]
        public DateTime? Fixed { get; set; }
        [Column("issue_creator_id")]
        public int IssueCreatorId { get; set; }
        [Column("task_id")]
        public int TaskId { get; set; }

        // Navigation properties

        public List<Employee>? Employees { get; set; }
        public Employee IssueCreator { get; set; }
        public Task Task { get; set; }
    }
}
