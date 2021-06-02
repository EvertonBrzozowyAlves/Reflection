using ByteBank.Portal.Infra;

namespace ByteBank.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixes = new string[] { "http://localhost:5341/" };
            var webApp = new WebApplication(prefixes);
            webApp.Start();
        }
    }
}
