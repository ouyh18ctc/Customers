using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Customers.Domain.Security
{
    public class CustomerMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        { get; set; }

        private IUserInformationRepository repository;

        public CustomerMembershipProvider(IUserInformationRepository repository)
            : base()
        {
            this.repository = repository;
        }

        public CustomerMembershipProvider()
            : this(new FakeUserRepository())
        { }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            UserInformation exitedUser = repository.UserInformations.FirstOrDefault(
                x => x.UserName == username && x.Password == oldPassword);
            if (exitedUser == null)
            { return false; }
            else 
            {
                exitedUser.Password = newPassword;
                repository.SaveChanges();
                return true;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, 
            string newPasswordQuestion, string newPasswordAnswer)
        {
            UserInformation exitedUser = repository.UserInformations.FirstOrDefault(
                x => x.UserName == username && x.Password == password);
            if (exitedUser == null)
            { return false; }
            else
            {
                exitedUser.PasswordQuestion = newPasswordQuestion;
                exitedUser.PasswordAnswer = newPasswordAnswer;
                repository.SaveChanges();
                return true;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email, 
            string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, 
            out MembershipCreateStatus status)
        {
            UserInformation existedUser = repository.AddUser(new UserInformation()
            {
                UserName = username,
                Password = password,
                EMail = email,
                PasswordQuestion = passwordQuestion,
                PasswordAnswer = passwordAnswer,
                IsApproved = isApproved,
                CityPermissioin = (int)providerUserKey
            });
            if (existedUser == null)
            { status = MembershipCreateStatus.Success; }
            else { status = MembershipCreateStatus.DuplicateUserName; }
            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return repository.DeleteUser(username);
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, 
            int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, 
            int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            UserInformation user = repository.UserInformations.FirstOrDefault(
                x => x.UserName == username && x.PasswordAnswer == answer);
            return (user == null) ? null : user.Password;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 6; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            string oldPassword = GetPassword(username, answer);
            if (oldPassword == null) { return null; }
            else 
            { 
                ChangePassword(username, oldPassword, "Abcdef9*");
                return "Abcdef9*";
            }
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            return repository.UserInformations.FirstOrDefault(
                x => x.UserName == username && x.Password == password)
                != null;
        }
    }
}
