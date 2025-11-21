using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Models.ServiceModels.Response
{
    public class ApplicationItem
    {
        public int? ApplicationID { get; set; }
        public int? UserID { get; set; }
        public int? CourseID { get; set; }
        public int? StatusID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserGsm { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
