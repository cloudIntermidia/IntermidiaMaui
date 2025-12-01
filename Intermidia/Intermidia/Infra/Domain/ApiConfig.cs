//using Flunt.Notifications;

using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Models.Xamarin;
using Intermidia.Services.Contracts;

namespace Intermidia.Intermidia.Infra.Domain
{
    public static class ApiConfig
    {
        public static bool EnviarJsonCompactadoPedido { get; set; } = true;
        public static bool ReceberJsonCompactadoPedido { get; set; } = false;
        public static bool EnviarJsonCompactadoCliente { get; set; } = true;
        public static bool ReceberJsonCompactadoCliente { get; set; } = false;
    }
}
