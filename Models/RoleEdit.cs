using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using EManager3.Areas.Identity.Data;

namespace EManager3.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<EManager3User> Members { get; set; }
        public IEnumerable<EManager3User> NonMembers { get; set; }
    }
}
