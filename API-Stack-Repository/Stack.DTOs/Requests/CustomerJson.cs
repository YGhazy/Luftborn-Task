using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    public class CustomerJson
    {
        public string UserName { get; set; }
        public string Address { get; set; }
        public List<VehicleJson> Vehicles { get; set; }

    }
}
