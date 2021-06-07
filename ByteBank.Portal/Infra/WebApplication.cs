using System;
using System.Net;
using System.Reflection;
using System.Text;
using ByteBank.Portal.Controller;

namespace ByteBank.Portal.Infra
{
    public class WebApplication
    {
        public string[] _prefixes { get; set; }
        public WebApplication(string[] prefixes)
        {
            if (prefixes == null)
            {
                throw new ArgumentException(nameof(prefixes));
            }
            _prefixes = prefixes;
        }

        public void Start()
        {
            while (true)
            {
                HandleRequest();
            }

        }

        private void HandleRequest()
        {
            var httpListener = new HttpListener();

            foreach (var prefix in _prefixes)
            {
                httpListener.Prefixes.Add(prefix);
            }

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
                var handler = new ControllerRequestHandler();
                handler.Handle(response, path);
            }



            httpListener.Stop();
        }
    }
}