using System.ComponentModel.DataAnnotations;


namespace CaloriesAppBackend.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
