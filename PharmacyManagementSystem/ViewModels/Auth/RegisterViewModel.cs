using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.ViewModels.Auth
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [MinLength(8)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
