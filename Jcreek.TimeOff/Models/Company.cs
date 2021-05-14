using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jcreek.TimeOff.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        public decimal MaximumYearlyUnusedRolloverDays { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
