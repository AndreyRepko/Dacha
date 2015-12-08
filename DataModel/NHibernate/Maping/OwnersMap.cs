using Dacha.DataModel.NHibernate.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dacha.DataModel.NHibernate.Maping {
    
    
    public class OwnersMap : ClassMapping<Owner> {

        public OwnersMap()
        {
            Schema("public");
            Table("owners");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.SequenceHiLo, g => g.Params(new {sequence = "owners_id_seq" })));
            Property(x => x.Name);
            Property(x => x.Comments);
        }
    }
}
