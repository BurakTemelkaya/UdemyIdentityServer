namespace UdemyIdentityServer.AuthServer.Models
{
    public class CustomUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }    

        public string City { get; set; }
    }
}
