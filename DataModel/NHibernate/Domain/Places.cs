using Dacha.DataModel.Domain;
using Dacha.PropertyMappings;

namespace Dacha.DataModel.NHibernate.Domain {
    
    public class Places {
        public virtual long Id { get; protected set; }
        [PropertyMapping("Square")]
        public virtual double? Square { get; set; }
        public virtual Sector Sector { get; set; }
        [PropertyMapping("Place number")]
        public virtual string PlaceNumber { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual long? ElectricityId { get; set; }
    }
}
