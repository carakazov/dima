using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using MySocialNetwork.DAO;
using MySocialNetwork.DTO;

namespace MySocialNetwork.Validators
{
    public class NewLoginValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string login = value.ToString();
                SocialNetworkDbContext dbContext = new SocialNetworkDbContext();
                if (dbContext.Users.Select(u => u.Login).Contains(login))
                {
                    return false;
                }
            }
            return true;
        }
    }
}