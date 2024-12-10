
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stack.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

        public virtual List<Vehicle> Vehicles { get; set; }

    }


}
