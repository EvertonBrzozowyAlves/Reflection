using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ByteBank.Portal.Infra
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName] string fileName = null)
        {
            var type = GetType();
            var directoryName = type.Name.Replace("Controller", "");

            var completeResourceName = $"ByteBank.Portal.View.{directoryName}.{fileName}.html";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(completeResourceName);

            var streamReader = new StreamReader(resourceStream);
            var pageText = streamReader.ReadToEnd();

            return pageText;
        }
    }
}