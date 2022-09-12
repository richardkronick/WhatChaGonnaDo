using System;
using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Models
{
    public class ToDoItem
    {
        public int ToDoItemId { get; set; }

        public string ToDoTask { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsDisabled { get; set; }

        public int ToDoListId { get; set; }
    }
}