using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class Credentials
    {
        [Column("username")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string username { get; set;  }
        [Column("password")]
        [Required]
        [StringLength(50)]
        public string password { get; set; }
        [Column("roles")]
        [Required]
        [StringLength(50)]
        public string role { get; set; }
    }
}
