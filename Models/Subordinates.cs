using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using EManager3.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EManager3.Models;

namespace EManager3.Models
{
    public class Subordinates
    {
        [Key] 
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        public string UserId { get; set; }

        public string SubordinateId { get; set; }
        // public IEnumerable<EManager3User> Subordinates { get; set; }
        
        public EManager3User Users { get; set; } 
  
    }
}