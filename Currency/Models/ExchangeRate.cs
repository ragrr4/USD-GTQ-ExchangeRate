using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Currency.Models
{
    public class ExchangeRate
    {
        public int ID;
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public float Rate { get; set; }
        public DateTime WhenObtained;

    }
}