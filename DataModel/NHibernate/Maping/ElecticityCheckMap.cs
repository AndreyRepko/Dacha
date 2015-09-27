using Dacha.DataModel.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class ElecticityCheckMap : ClassMapping<ElecticityCheck> {
        
        public ElecticityCheckMap() {
			Table("electicity_check");
			Schema("public");
			Lazy(true);
			Id(x => x.Id, map => map.Generator(Generators.Assigned));
			Property(x => x.Check, map =>
			{
			    map.NotNullable(true);
                map.Type<DateType>();
            });
			Property(x => x.Value, map =>
			{
			    map.NotNullable(true);
			});
			Property(x => x.ElectricityId, map => { map.Column("electricity_id"); map.NotNullable(true); });
        }
    }
}
