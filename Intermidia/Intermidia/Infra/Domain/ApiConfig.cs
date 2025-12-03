//using Flunt.Notifications;

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
