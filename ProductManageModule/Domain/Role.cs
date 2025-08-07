using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementModule.Domain
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("role_name", TypeName = "varchar(255)")]
        public string RoleName { get; set; }

        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; }

        // Navigation property to User
        public ICollection<User> Users { get; set; }
    }
}
