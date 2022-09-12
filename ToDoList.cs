using System;
using System.Collections.Generic;

namespace DoWhatNow.Models
{
    public class ToDoList
    {
        public int ToDoListId { get; set; }

        public string Label { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }
    }
}