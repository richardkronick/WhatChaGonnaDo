using DoWhatNow.Models;
using System.ComponentModel.DataAnnotations;

namespace DoWhatNow.Requests
{
    public class CreateToDoListRequestVersion1
    {
        [MaxLength(100)]
        public string Label { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? CreatedBy { get; set; }
    }
}
