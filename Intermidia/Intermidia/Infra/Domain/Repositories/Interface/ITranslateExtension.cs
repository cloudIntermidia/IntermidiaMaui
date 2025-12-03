namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface ITranslateExtension
    {
        object ProvideValue(IServiceProvider serviceProvider);
        string GetMessage(string text);
    }
}
