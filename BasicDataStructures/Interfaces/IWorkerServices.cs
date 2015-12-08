using System;
using System.Collections.Generic;
using BasicDataStructures.DataStructures;

namespace BasicDataStructures.Interfaces
{
    public interface IWorkerServices
    {
        List<ItemPresenterViewModel> GetItemsPresenterForEntity(Type entity);

        object GetItemByTypeAndId(Type entity, long? id);
    }
}
