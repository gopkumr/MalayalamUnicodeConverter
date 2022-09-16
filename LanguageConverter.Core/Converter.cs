namespace MalayalamConverter.Core
{
    using System.Text;
    using System.Text.Encodings;

    public class Converter
    {
        public static string ConvertAsciiToMalayalamUnicode(string inputstring, string[] mapping)
        {
            string[] characters = { "\u0D46", "\u0D47", "\u0D48", "\u0D4D\u0D30" };
            StringBuilder malayalamUnicode = new();
            Encoding sourceEncoding = Encoding.ASCII;
            byte[] sourceBytes = sourceEncoding.GetBytes(inputstring);
            string h = String.Empty;
            var mappingList = mapping.Select(q => new { key = q.Split('=').First(), value = q.Split('=').Skip(1).First() });

            foreach (var asciiChar in sourceBytes)
            {
                var matchedMapping = mappingList.FirstOrDefault(q => q.key.Equals(asciiChar.ToString(), StringComparison.OrdinalIgnoreCase));
                if (matchedMapping != null)
                {
                    var destination = matchedMapping.value.Replace("\r", string.Empty);
                    if (asciiChar == 10)
                    {
                        malayalamUnicode.Append('\n');
                    }
                    else
                    {
                        if (characters.Contains(destination))
                            h = destination;
                        else
                        {
                            malayalamUnicode.Append(destination);
                            if (!string.IsNullOrEmpty(h))
                            {
                                malayalamUnicode.Append(h);
                                h = string.Empty;
                            }
                        }
                    }
                }

            }

            return malayalamUnicode.ToString();
        }

    }
}