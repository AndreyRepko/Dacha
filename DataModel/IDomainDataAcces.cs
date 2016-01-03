using System.Collections.Generic;
using BasicDataStructures.Interfaces;
using Dacha.DataModel.NHibernate.Domain;

namespace Dacha.DataModel
{
    public interface IDomainDataAcces : IWorkerServices

    {
        IList<Cars> SearchForCarNumber(string[] splitCarNumber);
    }
}
