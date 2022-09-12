using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Requests
{
    public class UpdateStatusRequest
    {
        public bool IsDisabled { get; set; }

        [MaxLength(100)]
        public string? ModifiedBy { get; set; }
    }
}
