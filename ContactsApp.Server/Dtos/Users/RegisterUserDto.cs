namespace ContactsApp.Server.Dtos.Users
{
    /// <summary>
    /// Reprezentuje dane użytkownika wymagane do rejestracji w systemie.
    /// </summary>
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
