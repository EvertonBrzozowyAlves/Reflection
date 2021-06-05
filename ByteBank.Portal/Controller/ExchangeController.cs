using System.IO;
using System.Reflection;
using ByteBank.Service;
using ByteBank.Service.Exchange;

namespace ByteBank.Portal.Controller
{
    public class ExchangeController
    {
        private IExchangeService _exchangeService;
        public ExchangeController()
        {
            _exchangeService = new ExchangeTestService();
        }

        public string MXN()
        {
            var finalValue = _exchangeService.Calculate("MXN", "BRL", 1);
            var completeResourceName = "ByteBank.Portal.View.Exchange.MXN.html";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(completeResourceName);

            var streamReader = new StreamReader(resourceStream);
            var pageText = streamReader.ReadToEnd();
            var finalPageText = pageText.Replace("VALOR_EM_REAIS", finalValue.ToString());

            return finalPageText;
        }
        public string USD()
        {
            return null;
        }
    }
}