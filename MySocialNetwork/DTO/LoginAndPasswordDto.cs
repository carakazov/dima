using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MySocialNetwork.Validators;
namespace MySocialNetwork.DTO
{
    [LogInValidator]
    public class LoginAndPasswordDto
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}