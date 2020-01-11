using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace SystemLuckyDraw.Models
{
    
    [Table("Users_Table")]
    public class Users
    {
        
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter the Valid Email address")]
        public string Email { get; set; }
        public  string Password { get; set; }
    }
}