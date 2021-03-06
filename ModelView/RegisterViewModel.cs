﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.ModelView
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [MaxLength(30, ErrorMessage ="User name is to long")]
        [Remote(action:"IsUserNameInUse", controller:"Account")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password" , ErrorMessage ="Password and Confirmation password don't match")]
        public string ConfirmPassword { get; set; }

    }
}
