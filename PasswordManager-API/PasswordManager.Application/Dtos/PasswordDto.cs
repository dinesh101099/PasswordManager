namespace PasswordManager.API.Dtos
{
    public class PasswordDto
    {
        public string Category { get; set; }
        public string App { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // Plain text
    }
}
