using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models.ServiceModels.Base;

namespace YazOkulu.Data.Models.ServiceModels.Rquest
{
    public class CourseSearch : SearchRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? Quota { get; set; }
        public int? FacultyID { get; set; }
        public int? DepartmentID { get; set; }
        public int? StatusID { get; set; }
        public int? CurrentQuota { get; set; }
    }
}
