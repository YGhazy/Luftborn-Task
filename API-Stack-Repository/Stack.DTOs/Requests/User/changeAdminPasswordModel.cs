using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stack.DTOs.Requests
{
    //Basic Registration Model . 
    public class changeAdminPasswordModel
    {

        public string AdminAccountID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public DateTime Date { get; set; }


    }
}
