using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //Basic Registration Model . 
    public class changeUserPasswordModel
    {

        public string AccountID { get; set; }

        public string NewPassword { get; set; }

        public string User_Token { get; set; }

        public DateTime Date { get; set; }


    }
}
