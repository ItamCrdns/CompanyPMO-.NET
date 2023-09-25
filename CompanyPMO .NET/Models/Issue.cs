﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPMO_.NET.Models
{
    public class Issue
    {
        [Column("issue_id")]
        public int IssueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        [Column("started_working")]
        public DateTimeOffset StartedWorking { get; set; }
        public DateTimeOffset Fixed { get; set; }
        [Column("issue_creator")]
        public int IssueCreator { get; set; }
        [Column("task_id")]
        public int TaskId { get; set; }
    }
}