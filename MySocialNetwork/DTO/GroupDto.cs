namespace MySocialNetwork.DTO
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ReaderProfileStates ReaderState { get; set; }
        public GroupRoles GroupRole { get; set; }
        
        public int GroupTypeId { get; set; }
    }
}