using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stack.DTOs.Models
{
    public class CreateCustomerModel
    {

        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }
}
