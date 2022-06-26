using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Common
    {
    }
    public class RangeDate
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class HashSalt {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }

    }
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class SaveImageInfo
    {
        public List<string> ImageList { get; set; }
        public List<string> ImageFullPath { get; set; }
    }
}
