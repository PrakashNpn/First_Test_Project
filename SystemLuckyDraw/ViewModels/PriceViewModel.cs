using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SystemLuckyDraw.Models;

namespace SystemLuckyDraw.ViewModels
{
    public class PriceViewModel
    {
        public WinningNumber WinningNumber { get; set; }
        public Price Price { get; set; }
    }
}