using Dacha.DataModel.Domain;
using Dacha.DataModel.NHibernate.Domain;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class PlacesMap : ClassMapping<Places> {
        
        public PlacesMap() {
			Schema("public");
            Table("places");
			Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.SequenceHiLo, g => g.Params(new { sequence = "places_id_seq" })));
            Property(x => x.Square);
            ManyToOne(x => x.Sector, x =>
            {
                x.Column("sector_id");
                x.NotNullable(true);
                x.Lazy(LazyRelation.Proxy);
            });
			Property(x => x.PlaceNumber, map => map.Column("place_number"));
            ManyToOne(x => x.Owner, x =>
            {
                x.Column("owner_id");
                x.NotNullable(false);
                x.Lazy(LazyRelation.Proxy);
            }); Property(x => x.ElectricityId, map => map.Column("electricity_id"));
        }
    }
}
