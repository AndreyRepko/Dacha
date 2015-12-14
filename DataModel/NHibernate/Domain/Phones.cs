namespace Dacha.DataModel.NHibernate.Domain {
    
    public class Phones {
        public virtual long Id { get; set; }
        public virtual string Phone { get; set; }
        public virtual long? OwnerId { get; set; }
        public virtual string Comment { get; set; }
    }
}
