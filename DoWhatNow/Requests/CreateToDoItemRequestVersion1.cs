using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Requests
{
    public class CreateToDoItemRequestVersion1
    {
        public string ToDoTask { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? CreatedBy { get; set; }
    }
}
