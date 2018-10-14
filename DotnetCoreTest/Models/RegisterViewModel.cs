namespace DotnetCoreTest.Models
{
    public class RegisterViewModel
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public GroupPrivilege Group { get; set; }
        
        public enum GroupPrivilege
        {
            User,
            Administrator
        }
    }
    
    
}