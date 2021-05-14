using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jcreek.TimeOff.Models
{
    public class TeamMember
    {
        [Key]
        public int TeamMemberId { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public string Name { get; set; }
        public ICollection<AnnualHolidayAllocation> AnnualHolidayAllocations { get; set; }
        public ICollection<Holiday> Holidays { get; set; }
        public Team Team { get; set; }
        public ICollection<Team> ManagedTeams { get; set; }
    }
}
