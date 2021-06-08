using System;
using System.Net;
using System.Text;
using ByteBank.Portal.Infra.Binding;
using ByteBank.Portal.Infra.Filter;

namespace ByteBank.Portal.Infra
{
    public class ControllerRequestHandler
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();
        private readonly FilterResolver _filterResolver = new FilterResolver();

        public void Handle(HttpListenerResponse response, string path)
        {
            var parts = path.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            var controllerName = parts[0];
            var actionName = parts[1];

            var fullControllerName = $"ByteBank.Portal.Controller.{controllerName}Controller";
            var controllerWrapper = Activator.CreateInstance("ByteBank.Portal", fullControllerName, new object[0]);
            var controller = controllerWrapper.Unwrap();

            // var methodInfo = controller.GetType().GetMethod(actionName);
            var actionBindInfo = _actionBinder.GetMethodInfo(controller, path);

            var filterResult = _filterResolver.VerifyFilters(actionBindInfo);

            if (filterResult.CanContinue)
            {
                var actionResult = (string)actionBindInfo.Invoke(controller);

                var fileBuffer = Encoding.UTF8.GetBytes(actionResult);

                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength64 = fileBuffer.Length;
                response.OutputStream.Write(fileBuffer, 0, fileBuffer.Length);
                response.OutputStream.Close();
            }
            else
            {
                response.StatusCode = 307; //temporary redirect
                response.RedirectLocation = "Error/Unexpected";
                response.OutputStream.Close();
            }

        }
    }
}