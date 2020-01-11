using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace SystemLuckyDraw.Models
{
   
    public class DatabaseContext : DbContext
    {
        public DbSet<Users> LoginUsers { get; set; }
        public DbSet<WinningNumber> winningNumbers { get; set; }
        public DbSet<Price> prices { get; set; }
       
    }
}