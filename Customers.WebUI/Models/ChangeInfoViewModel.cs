using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Customers.WebUI.Models
{
    public class ChangeInfoViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名：")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请输入电子邮箱")]
        [Display(Name = "新电子邮箱：")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "请输入正确格式的电子邮箱")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "请输入密码问题")]
        [Display(Name = "新密码问题：")]
        public string PasswordQuestion { get; set; }

        [Required(ErrorMessage = "请输入问题答案")]
        [Display(Name = "新问题答案：")]
        public string PasswordAnswer { get; set; }
    }
}