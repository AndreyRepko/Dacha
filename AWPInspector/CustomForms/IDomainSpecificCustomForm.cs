using Dacha.DataModel;
using WPF.Dictionaries.CustomForms;

namespace Dacha.Inspector.CustomForms
{
    public interface IDomainSpecificCustomForm: ICustomFormViewModel
    {
        IDomainDataAcces DataServices { get; set; }
    }
}
