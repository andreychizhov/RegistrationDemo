using PM.Infrastructure;

namespace PM.Model.Entities
{
    public abstract class Location : DictionaryEntity
    {
        public string Name { get; set; }
        public LocationType LocationType { get; set; }
    }
}
