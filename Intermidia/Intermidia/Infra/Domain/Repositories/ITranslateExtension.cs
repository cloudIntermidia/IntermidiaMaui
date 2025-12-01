using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface ITranslateExtension
    {
        object ProvideValue(IServiceProvider serviceProvider);
        string GetMessage(string text);
    }
}
