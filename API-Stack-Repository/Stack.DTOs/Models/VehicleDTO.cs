using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.DTOs.Models
{
    public class VehicleDTO
    {

        public string ID { get; set; }
        public string Number { get; set; }
        public string VIN { get; set; }
        public DateTime LastPing { get; set; }
        public string Status { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUserDTO Customer { get; set; }
    }
}
