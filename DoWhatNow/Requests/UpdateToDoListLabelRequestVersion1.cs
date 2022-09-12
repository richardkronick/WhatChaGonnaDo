using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Requests
{
    public class UpdateToDoListLabelRequestVersion1
    {
        [MaxLength(100)]
        public string NewLabel { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }
    }
}
