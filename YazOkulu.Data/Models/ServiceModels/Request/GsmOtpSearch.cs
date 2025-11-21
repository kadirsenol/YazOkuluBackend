using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models.ServiceModels.Base;

namespace YazOkulu.Data.Models.ServiceModels.Request
{
    public class GsmOtpSearch : SearchRequest
    {
        public string? Name { get; set; } = string.Empty;
        public string? Gsm { get; set; } = string.Empty;
        public string? Otp { get; set; } = string.Empty;
    }
}
