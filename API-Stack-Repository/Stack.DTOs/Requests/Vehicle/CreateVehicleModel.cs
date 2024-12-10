using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.DTOs.Models
{
    public class CreateVehicleModel
    {

        public string Number { get; set; }
        public string VIN { get; set; }
        public string CustomerID { get; set; }


    }
}
