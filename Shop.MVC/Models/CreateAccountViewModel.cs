namespace Shop.MVC.Models
{
    public class CreateAccountViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public bool RememberLogin { get; set; }
        public string returnUrl { get; set; }
    }
}
