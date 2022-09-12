using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Requests
{
    public class UpdateToDoItemToggleRequestVersion1
    {
        public bool IsCompleted { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }
    }
}
