using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Models
{
    public class ToDoList
    {
        public int ToDoListId { get; set; }

        [MaxLength(100)]
        public string Label { get; set; } = string.Empty;

        public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsDisabled { get; set; }
    }
}