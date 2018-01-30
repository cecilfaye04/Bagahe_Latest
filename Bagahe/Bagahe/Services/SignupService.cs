using Bagahe.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bagahe.Models;
using Acr.UserDialogs;

namespace Bagahe.Services
{
    class SignupService : ISignupService
    {
        private Task addDelay()
        {
            return Task.Delay(2000);
        }

        async public Task<bool> AddNewUser(SignupFieldsModel SignupField)
        {
            bool isAdded = false;
            //Code for adding the inputs to the database here...
            //Check if the inputs are existing in the DB
            await addDelay();

            //Code for validating if the inputs are successfully added to the db here ...
            isAdded = true;

            return isAdded;
        }

        async public Task<List<UserInfoModel>> RetrieveUser(SignupFieldsModel SignupField)
        {
            await addDelay();
            
            //Mock existing accounts in DB
            List<UserInfoModel> mockUser = new List<UserInfoModel>();
            UserInfoModel userOne = new UserInfoModel();
            userOne.Username = "Bagahe";
            userOne.Email = "bagahe@gmail.com";
            mockUser.Add(userOne);

            UserInfoModel userTwo = new UserInfoModel();
            userTwo.Username = "Baggage";
            userTwo.Email = "baggage@gmail.com";
            mockUser.Add(userTwo);
            return mockUser;
        }
        
    }
}
