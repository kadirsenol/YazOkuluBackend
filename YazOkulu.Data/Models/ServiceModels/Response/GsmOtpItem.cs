using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Models.ServiceModels.Response
{
    public class GsmOtpItem
    {
        public int? GsmOtpID { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Gsm { get; set; } = string.Empty;
        public string? Otp { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
