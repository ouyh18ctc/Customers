using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Customers.WebUI.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [Display(Name = "用户名：")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入旧密码")]
        [StringLength(100, ErrorMessage = "{0} 长度至少为 {2} 个字符！", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入新密码")]
        [StringLength(100, ErrorMessage = "{0} 长度至少为 {2} 个字符！", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

    }
}