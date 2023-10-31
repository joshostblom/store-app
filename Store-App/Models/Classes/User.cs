using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public User(Guid id, string name, string emailAddress, string password)
        {
            this.Id = id;
            this.Name = name;
            this.EmailAddress = emailAddress;
            this.Password = password;
        }
    }
}
