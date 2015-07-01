using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.WebUI.Models;
using System.Web.Security;
using Customers.Domain.Security;
using Customers.Domain.ViewHelper;
using Customers.Domain.TypeDef;

namespace Customers.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserInformationRepository repository;

        public AccountController(IUserInformationRepository repository)
        {
            this.repository = repository;
            ListProviders.SetListProvider(() => new DemandListProvider());
        }

        private string UrlReferrerCalculate()
        {
            return Request.UrlReferrer != null ? Request.UrlReferrer.ToString()
                            : FormsAuthentication.DefaultUrl;
        }

        public ActionResult Login()
        {
            ViewBag.Title = "用户登录";
            return View(new UserLoginViewModel(){
                ReturnUrl = UrlReferrerCalculate()
            });
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.UserName, viewModel.Password))
                {
                    TempData["success"] = "登录成功！";
                    FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
                    return this.Redirect(viewModel.ReturnUrl);
                }
                else
                {
                    TempData["error"] = "登录失败! 请确认您输入的账号和密码是否正确！";
                }
            }
            else
            {
                TempData["warning"] = "部分字段不合要求，请重新填写！";
            }
            ViewBag.Title = "用户登录";
            return View(new UserLoginViewModel() { ReturnUrl = viewModel.ReturnUrl });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return this.Redirect(UrlReferrerCalculate());
        }

        public ActionResult Register(string username)
        {
            ViewBag.Title = "注册新用户";
            return View(new RegisterViewModel() { UserName = username });
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus membershipCreateStatus = new MembershipCreateStatus();
                Membership.CreateUser(viewModel.UserName, viewModel.Password, viewModel.EMail, 
                    viewModel.PasswordQuestion, viewModel.PasswordAnswer, true,
                    0, out membershipCreateStatus);
                if (membershipCreateStatus == MembershipCreateStatus.Success)
                {
                    TempData["success"] = viewModel.UserName + "用户注册成功！";
                    FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
                    return this.Redirect("/");
                }
                else
                {
                    TempData["error"] = "由于用户已存在或其他原因，注册用户失败，请重试！";
                }
            }
            ViewBag.Title = "注册新用户";
            return View(new RegisterViewModel() { UserName = viewModel.UserName });
        }

        public ActionResult ForgotPassword()
        {
            ViewBag.Title = "密码重置";
            return View(new ResetPasswordViewModel());
        }

        [HttpPost]
        public ActionResult ForgotPassword(ResetPasswordViewModel viewModel)
        {
            ViewBag.Title = "密码重置";
            if (ModelState.IsValid)
            {
                UserInformation user = repository.UserInformations.FirstOrDefault(
                        x => x.UserName == viewModel.UserName);
                if (user == null)
                {
                        TempData["error"] = "用户名不存在，请重新输入！";
                        return View(new ResetPasswordViewModel());
                }
                if (string.IsNullOrEmpty(viewModel.PasswordAnswer))
                {
                    TempData["info"] = "请回答密码重置问题的答案。";
                    return View(new ResetPasswordViewModel()
                    {
                        UserName = user.UserName,
                        PasswordQuestion = user.PasswordQuestion
                    });
                }
                else
                {
                    string oldPassword = Membership.Provider.ResetPassword(
                        viewModel.UserName, viewModel.PasswordAnswer);
                    if (string.IsNullOrEmpty(oldPassword))
                    {
                        TempData["error"] = "密码问题答案不正确，请重新输入！";
                        return View(new ResetPasswordViewModel()
                        {
                            UserName = viewModel.UserName,
                            PasswordQuestion = user.PasswordQuestion
                        });
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.UserName, true);
                        TempData["info"] = "密码重置成功！请继续修改密码。";
                        return this.RedirectToAction("ChangePassword", new
                        {
                            username = viewModel.UserName,
                            password = oldPassword
                        });
                    }
                }
            }
            else
            {
                TempData["error"] = "输入有误，请重试！";
                return View(new ResetPasswordViewModel() { UserName = viewModel.UserName });
            }
        }

        [Authorize]
        public ActionResult ChangePassword(string username, string password = null)
        {
            ViewBag.Title = "修改密码";
            return View(new ChangePasswordViewModel()
            {
                UserName = username,
                OldPassword = password
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.Provider.ChangePassword(viewModel.UserName, viewModel.OldPassword,
                    viewModel.NewPassword))
                {
                    TempData["success"] = "密码修改成功！";
                    return this.Redirect("/");
                }
                else
                {
                    TempData["error"] = "密码修改失败！";
                    return ChangePassword(viewModel.UserName, viewModel.OldPassword);
                }
            }
            TempData["error"] = "输入有误，请重试！";
            return ChangePassword(viewModel.UserName, viewModel.OldPassword);
        }

        [Authorize]
        public ActionResult ChangeInfo(string username)
        {
            ViewBag.Title = "修改信息";
            return View(new ChangeInfoViewModel()
            {
                UserName = username
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeInfo(ChangeInfoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.Provider.ChangePasswordQuestionAndAnswer(viewModel.UserName, viewModel.Password,
                    viewModel.PasswordQuestion, viewModel.PasswordAnswer))
                {
                    TempData["success"] = "修改信息成功！";
                    UserInformation info = repository.UserInformations.FirstOrDefault(
                        x => x.UserName == viewModel.UserName);
                    if (info != null) 
                    {
                        info.EMail = viewModel.EMail;
                        repository.SaveChanges();
                    }
                    return this.Redirect("/");
                }
                else
                {
                    TempData["error"] = "信息修改失败！";
                    return ChangeInfo(viewModel.UserName);
                }
            }
            TempData["error"] = "输入有误，请重试！";
            return ChangeInfo(viewModel.UserName);
        }

        [Authorize(Users = "admin")]
        public ActionResult ManageUsers()
        {
            return View(repository.UserInformations);
        }

        [Authorize(Users = "admin")]
        public ActionResult ModifyPermission(string username)
        {
            ViewBag.Title = username + "访问权限修改";
            UserInformation user = repository.UserInformations.FirstOrDefault(
                x => x.UserName == username);
            if (user == null)
            {
                return this.Redirect("ManageUsers");
            }
            return View(new ModifyPermissionViewModel()
            {
                UserName = username,
                Cities = user.PermissionList.Select(x => ((CityDef)x).GetCityName())
            });
        }

        [Authorize(Users = "admin")]
        [HttpPost]
        public ActionResult ModifyPermission(ModifyPermissionViewModel viewModel)
        {
            UserInformation user = repository.UserInformations.FirstOrDefault(
                x => x.UserName == viewModel.UserName);
            if (user == null)
            {
                TempData["error"] = "该用户不存在！";
                return this.Redirect("ManageUsers");
            }
            user.PermissionList = viewModel.Cities.Select(x => (int)x.GetCityIndex());
            repository.SaveChanges();
            TempData["success"] = "修改权限成功！";
            return this.Redirect("ManageUsers");
        }
    }
}
