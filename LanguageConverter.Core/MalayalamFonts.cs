using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConverter.Core
{
    public class MalayalamFonts
    {
        private static string GetResourceData(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"MalayalamConverter.Core.{fileName}";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
               return reader.ReadToEnd();
            }
        }

        public static List<string> GetAllFonts()
        {
            var configFile = GetResourceData("FontTemplateMapping.json");
            var array = JArray.Parse(configFile);

            return array.Select(q => q.Value<string>("font")??String.Empty).ToList();
        }

        private static string GetMapFileNameForFont(string fontName)
        {
            var configFile = GetResourceData("FontTemplateMapping.json");
            var array = JArray.Parse(configFile);

            var matchingMapping= array.Where(q => (q.Value<string>("font") ?? String.Empty).Equals(fontName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (matchingMapping != null)
                return $"{matchingMapping.Value<string>("map")}.map";

            return String.Empty;
        }

        private static string[] GetMapFileContent(string mapFileName)
        {
            var mapFile = GetResourceData($"mapfile.{mapFileName}");

            return mapFile.Split("\n");
        }

        public static string[] GetMapContentForFont(string font)
        {
            var mapFileName = GetMapFileNameForFont(font);
            var mapFileContent = GetMapFileContent(mapFileName);

            return mapFileContent;
        }

    }  
}
