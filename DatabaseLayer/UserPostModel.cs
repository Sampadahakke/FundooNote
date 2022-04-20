using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer
{
     public class UserPostModel
     {
       public string firstName { get; set; }
       public string lastName { get; set; }
       public string email { get; set; }
       public DateTime registerdDate { get; set; }
       public long password { get; set; }
       public string address { get; set; }
     }
    
}
