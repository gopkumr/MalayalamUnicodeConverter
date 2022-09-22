namespace MalayalamConverter.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings;
    using static System.Net.Mime.MediaTypeNames;

    public class Converter
    {
        public static string ConvertAsciiToMalayalamUnicode(string inputstring, string[] mapping)
        {
            string[] characters = { "\u0D46", "\u0D47", "\u0D48", "\u0D4D\u0D30" };
            StringBuilder malayalamUnicode = new();
            Encoding sourceEncoding = Encoding.Unicode;
            byte[] sourceBytes = sourceEncoding.GetBytes(inputstring);
            string h = String.Empty;
            var mappingList = mapping.Select(q => new { key = q.Split('=').First(), value = q.Substring(q.IndexOf('=') + 1, q.Length - (q.IndexOf('=') + 1)) }).ToList();

            foreach (var asciiChar in sourceBytes)
            {
                var matchedMapping = mappingList.FirstOrDefault(q => q.key.Equals(asciiChar.ToString(), StringComparison.OrdinalIgnoreCase));
                if (matchedMapping != null)
                {
                    var destination = matchedMapping.value.Replace("\r", string.Empty);

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
                else if (asciiChar == 10 || asciiChar == 13)
                {
                    malayalamUnicode.Append('\n');
                }

            }

            return malayalamUnicode.ToString();
        }

        public static string ConvertMalayalamUnicodeToAscii(string inputstring, string[] mapping)
        {
            string[] characters = { "\u0D00", "\u0D01", "\u0D02", "\u0D03", "\u0D04", "\u0D05", "\u0D06", "\u0D07", "\u0D08", "\u0D09", "\u0D0A", "\u0D0B", "\u0D0C", "\u0D0D", "\u0D0E", "\u0D0F", "\u0D10", "\u0D11", "\u0D12", "\u0D13", "\u0D14", "\u0D15", "\u0D16", "\u0D17", "\u0D18", "\u0D19", "\u0D1A", "\u0D1B", "\u0D1C", "\u0D1D", "\u0D1E", "\u0D1F", "\u0D20", "\u0D21", "\u0D22", "\u0D23", "\u0D24", "\u0D25", "\u0D26", "\u0D27", "\u0D28", "\u0D29", "\u0D2A", "\u0D2B", "\u0D2C", "\u0D2D", "\u0D2E", "\u0D2F", "\u0D30", "\u0D31", "\u0D32", "\u0D33", "\u0D34", "\u0D35", "\u0D36", "\u0D37", "\u0D38", "\u0D39", "\u0D3A", "\u0D3B", "\u0D3C", "\u0D3D", "\u0D3E", "\u0D3F", "\u0D40", "\u0D41", "\u0D42", "\u0D43", "\u0D44", "\u0D45", "\u0D46", "\u0D47", "\u0D48", "\u0D49", "\u0D4A", "\u0D4B", "\u0D4C", "\u0D4D", "\u0D4E", "\u0D4F", "\u0D50", "\u0D51", "\u0D52", "\u0D53", "\u0D54", "\u0D55", "\u0D56", "\u0D57", "\u0D58", "\u0D59", "\u0D5A", "\u0D5B", "\u0D5C", "\u0D5D", "\u0D5E", "\u0D5F", "\u0D60", "\u0D61", "\u0D62", "\u0D63", "\u0D64", "\u0D65", "\u0D66", "\u0D67", "\u0D68", "\u0D69", "\u0D6A", "\u0D6B", "\u0D6C", "\u0D6D", "\u0D6E", "\u0D6F", "\u0D70", "\u0D71", "\u0D72", "\u0D73", "\u0D74", "\u0D75", "\u0D76", "\u0D77", "\u0D78", "\u0D79", "\u0D7A", "\u0D7B", "\u0D7C", "\u0D7D", "\u0D7E", "\u0D7F" };
            string[] chillaksharam = { "ഈ", "ഊ", "ഐ", "ഓ", "ഔ", "ൈ", "ൊ", "ോ", "ൌ" };
            StringBuilder asciiText = new();
            string[] cha;
            //ML-TTAmbili Italic
            try
            {

                foreach (var a in chillaksharam)
                {
                    if (!asciiText.ToString().Contains(a))
                    {
                        if (a == "ഈ")
                        {
                            inputstring = inputstring.Replace("ഈ", "ഇൗ");
                        }
                        else if (a == "ഊ")
                        {
                            inputstring = inputstring.Replace("ഊ", "ഉൗ");
                        }
                        else if (a == "ഐ")
                        {
                            inputstring = inputstring.Replace("ഐ", "എെ");
                        }
                        else if (a == "ഓ")
                        {
                            inputstring = inputstring.Replace("ഓ", "ഒാ");
                        }
                        else if (a == "ഔ")
                        {
                            inputstring = inputstring.Replace("ഔ", "ഒൗ");
                        }
                        else if (a == "ൈ")
                        {
                            inputstring = inputstring.Replace("ൈ", "െെ");
                        }
                        else if (a == "ൊ")
                        {
                            inputstring = inputstring.Replace("ൊ", "ൊ");
                        }
                        else if (a == "ോ")
                        {
                            inputstring = inputstring.Replace("ോ", "ോ");
                        }
                        else if (a == "ൌ")
                        {
                            inputstring = inputstring.Replace("ൌ", "ൌ");
                        }
                    }
                }

                var array = new List<string>();
                var skip = false;
                for (var k = 0; k < inputstring.Count(); k++)
                {
                    var ind = 1;
                    var place_true = true;
                    var num = 2;
                    if (!skip)
                    {
                        if (new List<string> { "െ", "േ" }.Contains((inputstring[k]).ToString()))
                        {
                            ind = 1;
                            place_true = true;
                            num = 2;
                            while (place_true)
                            {

                                if (array.Count > 2 && new List<string> { "്" }.Contains((array[(array.Count - num)])))
                                {
                                    ind = ind + 2;
                                }
                                else if (!((array.ToArray()[(array.Count - (num + 1))..(array.Count - (num - 1))]).Except((new List<string> { " ", "്ര" }).ToArray()).Any()))
                                {
                                    ind = ind + 1;
                                }
                                else
                                {
                                    place_true = false;
                                }

                                num = num + 2;
                                if (array.Count <= num)
                                    num = array.Count - 1;

                            }
                            array.Insert((array.Count - ind), inputstring[k].ToString());
                        }
                        else if ((new List<char[]> { new char[] { '്', 'ര' }}).Contains(inputstring.ToArray()[k..((k + 2) > inputstring.Length ? inputstring.Length : k + 2)]))
                        {
                            ind = 1;
                            place_true = true;
                            num = 2;
                            while (place_true)
                            {
                                if (array.Count > 2 && new List<string> { "്" }.Contains(array[(array.Count - num)]))
                                {
                                    ind = ind + 2;
                                }
                                else
                                {
                                    place_true = false;
                                }
                                num = num + 1;
                             }
                            skip = true;
                            array.Insert(array.Count - ind, String.Join("", inputstring[k..(k + 2)]));
                        }
                        else
                        {
                            array.Add(inputstring[k].ToString());
                        }
                    }
                    else
                    {
                        skip = false;
                    }
                }

                inputstring = string.Join("", array);

                for (var j = 0; j < mapping.Count(); j++)
                {
                    cha = mapping[j].Split("=");
                    if (cha.Length > 1)
                    {
                        asciiText.Append(cha[1]);
                        if (cha[1].Count() >= 5)
                        {
                            if (inputstring.Contains(cha[1].Replace("\r", "")))
                            {
                                var sourceCharacter = cha[1].Replace("\r", "");
                                var destinationCharacter = Convert.ToChar(Convert.ToInt32(cha[0])).ToString();
                                inputstring = inputstring.Replace(sourceCharacter, destinationCharacter);
                            }
                        }
                    }
                }

                for (var j = 0; j < mapping.Count(); j++)
                {
                    cha = mapping[j].Split("=");
                    if (cha.Length > 1)
                    {
                        asciiText.Append(cha[1]);
                        if (cha[1].Count() >= 4)
                        {
                            if (inputstring.Contains(cha[1].Replace("\r", "")))
                            {
                                var sourceCharacter = cha[1].Replace("\r", "");
                                var destinationCharacter = Convert.ToChar(Convert.ToInt32(cha[0])).ToString();
                                inputstring = inputstring.Replace(sourceCharacter, destinationCharacter);
                            }
                        }
                    }
                }
                for (var j = 0; j < mapping.Count(); j++)
                {
                    cha = mapping[j].Split("=");
                    if (cha.Length > 1)
                    {
                        asciiText.Append(cha[1]);
                        if (cha[1].Count() >= 3)
                        {
                            if (inputstring.Contains(cha[1].Replace("\r", "")))
                            {
                                var sourceCharacter = cha[1].Replace("\r", "");
                                var destinationCharacter = Convert.ToChar(Convert.ToInt32(cha[0])).ToString();
                                inputstring = inputstring.Replace(sourceCharacter, destinationCharacter);
                            }
                        }
                    }
                }
                for (var j = 0; j < mapping.Count(); j++)
                {
                    cha = mapping[j].Split("=");
                    if (cha.Length > 1)
                    {
                        asciiText.Append(cha[1]);
                        if (cha[1].Count() >= 2)
                        {
                            if (inputstring.Contains(cha[1].Replace("\r", "")))
                            {
                                var sourceCharacter = cha[1].Replace("\r", "");
                                var destinationCharacter = Convert.ToChar(Convert.ToInt32(cha[0])).ToString();
                                inputstring = inputstring.Replace(sourceCharacter, destinationCharacter);
                            }
                        }
                    }
                }
                for (var j = 0; j < mapping.Count(); j++)
                {
                    var chars = mapping[j].Split("=");
                    if (chars.Length > 1)
                    {
                        if (chars[1].Replace("\r", "") != "")
                        {
                            if (inputstring.Contains(chars[1].Replace("\r", "")))
                            {
                                var sourceCharacter = chars[1].Replace("\r", "");
                                var destinationCharacter = Convert.ToChar(Convert.ToInt32(chars[0])).ToString();
                                inputstring = inputstring.Replace(sourceCharacter, destinationCharacter);
                            }
                        }
                    }
                }
                return inputstring;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}