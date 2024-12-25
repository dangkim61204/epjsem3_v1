using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_DAL.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string? Avata { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Education { get; set; }
        public string? Grade { get; set; }
        public string? Achievements { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        public virtual ICollection<ClientEmployee>? ClientEmployees { get; set; }


    }
    
}
