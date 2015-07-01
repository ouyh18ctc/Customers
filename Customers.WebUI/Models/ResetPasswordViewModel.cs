using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Customers.WebUI.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名：")]
        public string UserName { get; set; }

        [Display(Name = "密码问题：")]
        public string PasswordQuestion { get; set; }

        [Display(Name = "问题答案：")]
        public string PasswordAnswer { get; set; }
    }
}