using Dacha.DataModel.Domain;
using Dacha.PropertyMappings;

namespace Dacha.DataModel.NHibernate.Domain {
    
    public class Places {
        public virtual long Id { get; protected set; }

        [PropertyMapping("Square")]
        public virtual double? Square { get; set; }

        [PropertyMapping("Sector")]
        [PropertyLink(typeof(Sector))]
        public virtual Sector Sector { get; set; }

        [PropertyMapping("Place number")]
        public virtual string PlaceNumber { get; set; }

        [PropertyMapping("Owner")]
        [PropertyLink(typeof(Owner))]
        public virtual Owner Owner { get; set; }

        public virtual long? ElectricityId { get; set; }
    }
}
