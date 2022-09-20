namespace MalayalamConverter.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MalayalamConversionUnicodeTest()
        {
            var malayalamunitcode = "മലയലം";
            var ascii= "aebew";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertAsciiToMalayalamUnicode(ascii, mapping);

            Assert.That(malayalamunitcode.Equals(convertedText));
;
        }

        [Test]
        public void MalayalamConversionAsciiTest()
        {
            var malayalamunitcode = "മലയലം";
            var ascii = "aebew";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(malayalamunitcode, mapping);

            Assert.That(ascii.Equals(convertedText));
            ;
        }
    }
}