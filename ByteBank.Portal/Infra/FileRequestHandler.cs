using System.Net;
using System.Reflection;

namespace ByteBank.Portal.Infra
{
    public class FileRequestHandler
    {
        public void Handle(HttpListenerResponse response, string path)
        {
            var assembly = Assembly.GetExecutingAssembly(); //get metada from the executing assembly
            var resourceName = Utils.ConvertPathNameToAssemblyName(path);

            //stream can be understood as data flow. 
            // you do not deal with a huge file at once (6GB, for example), you work with pieces of the file 
            var resourceStream = assembly.GetManifestResourceStream(resourceName);

            if (resourceStream == null)
            {
                response.StatusCode = 404;
                response.OutputStream.Close();
            }
            using (resourceStream)
            {
                var bytesResource = new byte[resourceStream.Length];

                resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                response.ContentType = Utils.GetContentType(path);

                //always use stream for http listener
                response.StatusCode = 200;
                response.ContentLength64 = resourceStream.Length;

                response.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                response.OutputStream.Close();
            }
        }
    }
}