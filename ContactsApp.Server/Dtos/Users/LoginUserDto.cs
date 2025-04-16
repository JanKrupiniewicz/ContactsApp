namespace ContactsApp.Server.Dtos.Users
{
    /// <summary>
    /// Reprezentuje dane logowania użytkownika.
    /// </summary>
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
