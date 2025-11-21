using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Models.ServiceModels.DTO
{
    public class GsmOtpDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Gsm { get; set; } = string.Empty;
        public string? Otp { get; set; } = string.Empty;
    }
}
