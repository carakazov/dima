using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;
using MySocialNetwork.Utils;

namespace MySocialNetwork.Validators
{
    public class LogInValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
                LoginAndPasswordDto loginAndPasswordDto = (LoginAndPasswordDto) value;
                string login = loginAndPasswordDto.Login;
                string password = Cryptographer.Hash(loginAndPasswordDto.Password);
                if (dbContext.Users.Select(u => u.Login).Contains(login))
                {
                    string actualPassword = dbContext.Users.Where(u => u.Login == login).First().Password;
                    if (actualPassword == password)
                    {
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Wrong password";
                        return false;
                    }
                }
                else
                {
                    ErrorMessage = "Wrong login";
                    return false;
                }
            }
            return false;
        }
    }
}