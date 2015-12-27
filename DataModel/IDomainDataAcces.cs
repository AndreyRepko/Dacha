using BasicDataStructures.Interfaces;
using Dacha.DataModel.NHibernate.Domain;

namespace Dacha.DataModel
{
    public interface IDomainDataAcces : IWorkerServices

    {
        Cars SearchForCarNumber(string[] splitCarNumber);
    }
}
