using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //Basic Registration Model . 
    public class RegisterModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }



    }
}
