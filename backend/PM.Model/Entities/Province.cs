namespace PM.Model.Entities
{
    public class Province : Location
    {
        public int? ParentId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ClientInfo ClientInfo { get; set; }
    }
}
