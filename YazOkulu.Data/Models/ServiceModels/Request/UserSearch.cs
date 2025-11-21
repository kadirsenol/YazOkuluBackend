using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models.ServiceModels.Base;

namespace YazOkulu.Data.Models.ServiceModels.Request
{
    public class UserSearch : SearchRequest
    {
        public string Gsm { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int? FacultyID { get; set; }
        public int? DepartmentTypeID { get; set; }
        public int? RoleTypeID { get; set; }
    }
}
