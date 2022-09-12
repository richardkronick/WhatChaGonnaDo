using System;

namespace DoWhatNow.Models
{
    public class ToDoItem
    {
        public int ToDoItemId { get; set; }

        public string Task { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}