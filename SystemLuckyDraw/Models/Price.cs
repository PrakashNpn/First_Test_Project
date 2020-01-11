using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemLuckyDraw.Models
{
    [Table("Price_Table")]
    public class Price
    {
        [Key]
        public int Id { get; set; }
       
        public string Email { get; set; }
        
        public string   WinningNo { get; set; }
        public string TypeOfPrize { get; set; }

    }
}