using Dacha.DataModel.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class ElectricityMap : ClassMapping<Electricity> {

        public ElectricityMap()
        {
            Schema("public");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Assigned));
            Property(x => x.Electirc, map => map.NotNullable(true));
            Property(x => x.Outdoor, map => map.NotNullable(true));
            Property(x => x.Comment);
        }
    }
}
