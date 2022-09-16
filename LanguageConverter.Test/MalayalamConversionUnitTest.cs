namespace MalayalamConverter.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MalayalamConversionTest()
        {
            var malayalamunitcode = "മലയലം";
            var ascii= "aebew";
            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = MalayalamConverter.Core.Converter.ConvertAsciiToMalayalamUnicode(ascii, mapping);

            Assert.That(malayalamunitcode.Equals(convertedText));
;
        }
    }
}