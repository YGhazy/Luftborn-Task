
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Stack.DTOs.Models
{
    public class ApplicationUserDTO 
    {
        public string ID { get; set; }

        public string Address { get; set; }
        public string UserName { get; set; }
        public virtual List<VehicleDTO> Vehicles { get; set; }



    }

}
