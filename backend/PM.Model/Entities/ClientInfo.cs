using PM.Infrastructure;

namespace PM.Model.Entities
{
    public class ClientInfo : Entity<int>
    {
        public string FullAddress { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Country Country { get; set; }
        public virtual Province Province { get; set; }
    }
}