using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BairesDev.Models
{
    public class RolesWithPriorities
    {
        public RolesWithPriorities(string role, int priority)
        {

            Role = role;
            Priority = priority;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Role { get; set; }
        public int Priority { get; set; }
    }

}
