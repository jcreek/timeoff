using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jcreek.TimeOff.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }


        public string TeamName { get; set; }
        public ICollection<TeamMember> Users { get; set; }

        public Company Company { get; set; }
        public virtual ICollection<TeamMember> TeamManagers { get; set; }
    }
}
