using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Models.ServiceModels.DTO
{
    public class UserDto
    {
        public int? UserID { get; set; }
        public string Gsm { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int? FacultyTypeID { get; set; }
        public int? DepartmentTypeID { get; set; }
        public int? RoleTypeID { get; set; }
    }
}
