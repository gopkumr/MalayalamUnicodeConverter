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
        public void MalayalamConversionAsciiTest001()
        {
            var malayalamunitcode = "മലയലം";
            var ascii = "aebew";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(malayalamunitcode, mapping);

            Assert.That(ascii.Equals(convertedText));
            ;
        }

        [Test]
        public void MalayalamConversionAsciiTest002()
        {
            var malayalamunitcode = "എന്റെ";
            var ascii = "Fsâ";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(malayalamunitcode, mapping);

            Assert.That(ascii.Equals(convertedText));
            ;
        }

        [Test]
        public void MalayalamConversionAsciiTest003()
        {
            var malayalamunitcode = "മലര്";
            var ascii = "aecv";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(malayalamunitcode, mapping);

            Assert.That(ascii.Equals(convertedText));
            ;
        }

        [Test]
        public void MalayalamConversionAsciiTest004()
        {
            var malayalamunitcode = "ധൃതരാഷ്ട്രർ";
            var ascii = "[rXcmã{À";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(malayalamunitcode, mapping);

            Assert.That(ascii.Equals(convertedText));
            
        }
    }
}