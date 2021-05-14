using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jcreek.TimeOff.Models
{
    public class Holiday
    {
        [Key]
        public int HolidayId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double NumberOfDays
        {
            get
            {
                // todo - check this handles half days correctly
                TimeSpan difference = EndDate - StartDate;
                return difference.TotalDays;
            }
        }

        public bool IsApproved { get; set; }
        public TeamMember User { get; set; }
    }
}
