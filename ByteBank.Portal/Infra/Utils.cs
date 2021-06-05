using System;
using System.Linq;

namespace ByteBank.Portal.Infra
{
    public static class Utils
    {
        public const string AssemblyPrefix = "ByteBank.Portal";
        public static string ConvertPathNameToAssemblyName(string path)
        {
            return $"{AssemblyPrefix}{path.Replace('/', '.')}";
        }

        public static string GetContentType(string path)
        {
            if (path.EndsWith(".css"))
                return "text/css; charset=utf-8";
            if (path.EndsWith(".js"))
                return "application/js; charset=utf-8";
            if (path.EndsWith(".html"))
                return "text/html; charset=utf-8";

            throw new NotImplementedException("Tipo de conteúdo não previsto");
        }

        public static bool IsAFile(string path)
        {
            var pathParts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var lastPart = pathParts.Last();

            return lastPart.Contains('.');
        }
    }
}