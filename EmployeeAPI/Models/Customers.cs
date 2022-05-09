using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class Customers
    {
        [Column("fullName")]
        [StringLength(50)]
        public string fullName { get; set; }
        [Column("username")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("username")]
        [Required]
        [StringLength(50)]
        public string username { get; set; }
        [Column("DateOfJoining")]
        [StringLength(50)]
        public string dateOfJoining { get; set; }
        [Column("phoneNumber")]
        [StringLength(50)]
        public string phoneNumber { get; set; }
    }
}
