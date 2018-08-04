using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMEntity
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username Required !!")]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email required !!")]
        [DataType(DataType.EmailAddress)]
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide password !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public double TotalSpent { get; set; }
    }
}
