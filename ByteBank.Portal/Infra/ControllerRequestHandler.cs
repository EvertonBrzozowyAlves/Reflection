using System;
using System.Net;
using System.Text;
using ByteBank.Portal.Infra.Binding;

namespace ByteBank.Portal.Infra
{
    public class ControllerRequestHandler
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();

        public void Handle(HttpListenerResponse response, string path) //TODO: test
        {
            var parts = path.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            var controllerName = parts[0];
            var actionName = parts[1];

            var fullControllerName = $"ByteBank.Portal.Controller.{controllerName}Controller";
            var controllerWrapper = Activator.CreateInstance("ByteBank.Portal", fullControllerName, new object[0]);
            var controller = controllerWrapper.Unwrap();

            // var methodInfo = controller.GetType().GetMethod(actionName);
            var methodInfo = _actionBinder.GetMethodInfo(controller, path);
            var actionResult = (string)methodInfo.Invoke(controller);

            var fileBuffer = Encoding.UTF8.GetBytes(actionResult);

            response.StatusCode = 200;
            response.ContentType = "text/html; charset=utf-8";
            response.ContentLength64 = fileBuffer.Length;
            response.OutputStream.Write(fileBuffer, 0, fileBuffer.Length);
            response.OutputStream.Close();
        }
    }
}