using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dacha.Inspector.Dictionaries;

namespace Dacha.Inspector
{
    public interface IPresenterFactory
    {
        void PresentDictionary<T>(DictionaryViewModel<T>  dictionaryViewModel);
    }
}
