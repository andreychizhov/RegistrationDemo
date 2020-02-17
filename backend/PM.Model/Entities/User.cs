using PM.Infrastructure;

namespace PM.Model.Entities
{
    public class User : Entity<int>
    {
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ClientInfo ClientInfo { get; set; }
    }
}