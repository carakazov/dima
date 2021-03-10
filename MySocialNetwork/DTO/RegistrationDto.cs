using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MySocialNetwork.Validators;

namespace MySocialNetwork.DTO
{
    public class RegistrationDto
    {
        [Required(ErrorMessage = "It is required field")]
        [NewLoginValidator(ErrorMessage = "Login must be unique")]
        public string Login { get; set; }
        public string Password { get; set; }
        [MaxLength(60, ErrorMessage = "Name has to be longer than 60 letters and shorter than 2")]
        [MinLength(2, ErrorMessage = "Name has to be longer than 60 letters and shorter than 2")]
        [Required(ErrorMessage = "It is required field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "It is required field")]
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "It is required field")]
        public DateTime BirthDate { get; set; }
    }
}