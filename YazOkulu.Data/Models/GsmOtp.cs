using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Base;

namespace YazOkulu.Data.Models
{
    public class GsmOtp : BaseEntity
    {
        public int GsmOtpID { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }
}
