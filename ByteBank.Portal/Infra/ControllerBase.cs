using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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

        protected string View(object model, [CallerMemberName] string fileName = null)
        {
            var rawView = View(fileName);
            var modelProperties = model.GetType().GetProperties();

            var regex = new Regex("\\{{(.*?)\\}}");

            var view = regex.Replace(rawView, (match) =>
            {
                var propertyName = match.Groups[1].Value;
                var property = modelProperties.Single(p => p.Name == propertyName);

                var rawValue = property.GetValue(model); //returns the property value based on the instance
                return rawValue?.ToString();
            });

            return view;
        }
    }
}