namespace MySocialNetwork.DTO
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        
        public bool Banned { get; set; }
        
        public int Rating { get; set; }
        
        public int Age { get; set; }
    }
}