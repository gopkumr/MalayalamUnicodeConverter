namespace MalayalamConverter.UI.Views;

using MalayalamConverter.Core;
using MalayalamConverter.UI.Models;


public partial class UnicodeConverter : ContentPage
{
    public UnicodeConverter()
    {
        InitializeComponent();

        var viewModel = new Malayalam();
        viewModel.Fonts = MalayalamFonts.GetAllFonts().OrderBy(r=>r).Select(q => new MalayalamFont() { Name = q }).ToArray();
        this.BindingContext = viewModel;

    }

    private void btnConvert_Clicked(object sender, EventArgs e)
    {
        var sourceText = txtSourceText.Text;
        var font = (lstFont.SelectedItem as MalayalamFont).Name;

        var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont(font);

        var convertedText = MalayalamConverter.Core.Converter.ConvertAsciiToMalayalamUnicode(sourceText, mapping);

        txtDesinationText.Text= convertedText;


    }
}