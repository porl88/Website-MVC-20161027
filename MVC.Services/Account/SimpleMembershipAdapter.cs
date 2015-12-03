namespace MVC.Services.Account
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Core.Entities.Account;
    using WebMatrix.WebData;

    public class SimpleMembershipAdapter : IAccountService
    {
        public string CreateAccount(string email, string password)
        {
            return CreateAccount(email, password, email);
        }

        public string CreateAccount(string userName, string password, string email)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues: new { Email = email }, requireConfirmationToken: true);
        }

        public bool ActivateAccount(string activateAccountToken)
        {
            return WebSecurity.ConfirmAccount(activateAccountToken);
        }

        public void DeleteAccount(string userName)
        {
            //if (Roles.GetRolesForUser(userName).Count() > 0)
            //{
            //    Roles.RemoveUserFromRoles(userName, Roles.GetRolesForUser(userName));
            //}
			/*
            var membership = (SimpleMembershipProvider)Membership.Provider;
            membership.DeleteAccount(userName); // deletes record from webpages_Membership table
            membership.DeleteUser(userName, true); // deletes record from UserProfile table
			*/
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (WebSecurity.IsAuthenticated)
            {
                var userName = WebSecurity.CurrentUserName;
                return WebSecurity.ChangePassword(userName, oldPassword, newPassword);
            }

			return false;
        }

        public string ResetPasswordRequest(string userName, TimeSpan expires)
        {
			if (WebSecurity.UserExists(userName))
			{
				return WebSecurity.GeneratePasswordResetToken(userName, (int)expires.TotalMinutes);
			}

			return null;
        }

        public bool ResetPassword(string resetPasswordToken, string newPassword)
        {
            return WebSecurity.ResetPassword(resetPasswordToken, newPassword);
        }

        public bool LogIn(string userName, string password)
        {
            return WebSecurity.Login(userName, password);
        }

		public bool LogIn(string userName, string password, TimeSpan persistence)
        {
            System.Web.HttpContext.Current.Response.Cookies[0].Expires = DateTime.Now.Add(persistence);
            return WebSecurity.Login(userName, password, true);
        }

        public void LogOut()
        {
            WebSecurity.Logout();
        }

        public bool IsAuthenticated
        {
            get
            {
                return WebSecurity.IsAuthenticated;
            }
        }

        public User GetUser(string userName)
        {
            return new User
            {

            };
        }
    }
}
