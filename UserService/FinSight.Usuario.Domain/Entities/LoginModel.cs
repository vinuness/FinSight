namespace FinSight.Usuario.Domain.Entities
{
    public class LoginModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginResponse
    {
        public required string Email { get; set; }
        public required string Nome { get; set; }
        public required string Role { get; set; }
        public required string Token { get; set; }
    }
}
