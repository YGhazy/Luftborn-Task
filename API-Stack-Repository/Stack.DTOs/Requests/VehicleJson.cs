using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class VehicleJson
    {
        public string VIN { get; set; }
        public string Number { get; set; }
    }
}
