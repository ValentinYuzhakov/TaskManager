﻿using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Models
{
    public class ToDoTask : Entity
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskPriority TaskPriority { get; set; } = TaskPriority.None;
        public TaskStatus TaskStatus { get; set; } = TaskStatus.InProgress;
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public IList<SubTask> SubTasks { get; set; } = new List<SubTask>();
        public IList<TaskFolder> Folders { get; set; } = new List<TaskFolder>();
    }
}