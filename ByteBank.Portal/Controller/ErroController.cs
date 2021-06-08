using ByteBank.Portal.Infra;

namespace ByteBank.Portal.Controller
{
    public class ErroController : ControllerBase
    {
        public string Unexpected()
            => View();
    }
}