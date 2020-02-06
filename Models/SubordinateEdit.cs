using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using EManager3.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace EManager3.Models
{
    public class SubordinateEdit
    {
        public EManager3User User { get; set; }

        public IEnumerable<Subordinates> Members { get; set; }

        [Display(Name="Direct Subordinates")]
        public IEnumerable<EManager3User> Users { get; set; }
        // public IEnumerable<EManager3User> Subordinates { get; set; }
    }
}