using System;
using System.Net;
using ByteBank.Portal.Infra.IoC;
using ByteBank.Service;
using ByteBank.Service.Card;
using ByteBank.Service.Exchange;

namespace ByteBank.Portal.Infra
{
    public class WebApplication
    {
        public string[] _prefixes { get; set; }
        private readonly IContainer _container = new SimpleContainer();
        public WebApplication(string[] prefixes)
        {
            if (prefixes == null)
                throw new ArgumentException(nameof(prefixes));
            _prefixes = prefixes;
            Configure();
        }

        private void Configure()
        {
            _container.Register<IExchangeService, ExchangeTestService>();
            _container.Register<ICardService, CardTestService>();
        }

        public void Start()
        {
            while (true)
                HandleRequest();

        }

        private void HandleRequest()
        {
            var httpListener = new HttpListener();

            foreach (var prefix in _prefixes)
                httpListener.Prefixes.Add(prefix);

            httpListener.Start();

            var context = httpListener.GetContext();
            var request = context.Request;
            var response = context.Response;

            var path = request.Url.PathAndQuery;

            if (Utils.IsAFile(path))
            {
                var handler = new FileRequestHandler();
                handler.Handle(response, path);
            }
            else
            {
                var handler = new ControllerRequestHandler(_container);
                handler.Handle(response, path);
            }
            httpListener.Stop();
        }
    }
}