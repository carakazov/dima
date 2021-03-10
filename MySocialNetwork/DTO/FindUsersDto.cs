using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MySocialNetwork.DAO;

namespace MySocialNetwork.DTO
{
    public class FindUsersDto 
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}