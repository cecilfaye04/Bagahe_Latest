using Acr.UserDialogs;
using Bagahe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe.Services
{
    class ForgotPasswordService : IForgotPasswordService
    {
        async Task<bool> IForgotPasswordService.CheckEmail(string email)
        {
            bool isExisting = false;
            await addDelay();
            //checking the email to DB
            if (email.Equals("admin@admin.com"))
            {
                isExisting = true;
            }
               
            
            return isExisting;
        }

        private Task addDelay()
        {
            return Task.Delay(2000);
        }

        async public Task<bool> ChangePassword(string password)
        {
            bool updatePassword = true;
            //Code for updating pw to DB

            await addDelay();

            return updatePassword;
        }
    }
}
