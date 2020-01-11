using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemLuckyDraw.Models
{
    [Table("WinningNo_Table")]
    public class WinningNumber
    {
       
        public int Id { get; set; }
        public string Email { get; set; }

       
        public string WinningNo { get; set; }

    }
}