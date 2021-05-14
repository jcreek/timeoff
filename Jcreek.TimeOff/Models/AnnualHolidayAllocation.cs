using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jcreek.TimeOff.Models
{
    public class AnnualHolidayAllocation
    {
        [Key]
        public int AnnualHolidayAllocationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Year { get; set; }
        public int Days { get; set; }
        public TeamMember User { get; set; }
    }
}
