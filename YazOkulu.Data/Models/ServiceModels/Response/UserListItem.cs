using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Models.ServiceModels.Response
{
    public class UserListItem
    {
        public int? UserID { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int? FacultyTypeID { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public int? DepartmentTypeID { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int? RoleTypeID { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
