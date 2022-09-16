namespace LanguageConverter.Test
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
            var mapping = LanguageConverter.Core.MalayalamFonts.GetMapContentForFont("ML-TTKarthika Bold");

            var convertedText = LanguageConverter.Core.Converter.ConvertAsciiToMalayalamUnicode(ascii, mapping);

            Assert.AreEqual(malayalamunitcode, convertedText);
;
        }
    }
}