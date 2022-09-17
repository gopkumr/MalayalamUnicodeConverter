namespace MalayalamConverter.UI.Views;

using MalayalamConverter.Core;
using MalayalamConverter.UI.Models;


public partial class UnicodeConverter : ContentPage
{
    public UnicodeConverter()
    {
        InitializeComponent();

        var viewModel = new Malayalam();
        viewModel.Fonts = MalayalamFonts.GetAllFonts().OrderBy(r => r).Select(q => new MalayalamFont() { Name = q }).ToArray();
        this.BindingContext = viewModel;

    }

    private async void mnuLoadFile_Clicked(object sender, EventArgs e)
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".txt", ".doc" } },
                 });

            PickOptions options = new()
            {
                PickerTitle = "Please select a manayalam ascii ",
                FileTypes = customFileType,
            };


            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    using var streamReader = new StreamReader(stream);
                    txtSourceText.Text = await streamReader.ReadToEndAsync();
                }
            }

        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }
    }

    private void mnuExit_Clicked(object sender, EventArgs e)
    {

    }

    private void mnuConvert_Clicked(object sender, EventArgs e)
    {
        var sourceText = txtSourceText.Text;
        var font = (lstFont.SelectedItem as MalayalamFont).Name;

        var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont(font);

        var convertedText = MalayalamConverter.Core.Converter.ConvertAsciiToMalayalamUnicode(sourceText, mapping);

        txtDesinationText.Text = convertedText;
    }

    
}