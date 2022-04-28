using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer
{
    public  class PasswordValidation
    {
        //Providing validation for variables
        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$", ErrorMessage = "Password is not valid")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required(ErrorMessage = "{0} should not be empty")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,15})$", ErrorMessage = "Password is not valid")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}
