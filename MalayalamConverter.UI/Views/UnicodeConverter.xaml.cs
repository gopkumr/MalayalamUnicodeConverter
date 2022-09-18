namespace MalayalamConverter.UI.Views;

using MalayalamConverter.Core;
using MalayalamConverter.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class UnicodeConverter : ContentPage
{
    bool isDirty = false;
    string savedFiles = string.Empty;

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
            await DisplayAlert("Alert", $"Filed to load file: {ex.Message}", "OK");
        }
    }

    private async void mnuExit_Clicked(object sender, EventArgs e)
    {
        if (isDirty)
        {
            bool answer = await DisplayAlert("Question?", "Would you like to save your work before you close?", "Yes", "No");
            if (answer)
            {
                if (!string.IsNullOrEmpty(savedFiles))
                    SaveWork(savedFiles);
                else
                {
                    var customFileType = new FilePickerFileType(
               new Dictionary<DevicePlatform, IEnumerable<string>>
               {
                    { DevicePlatform.WinUI, new[] { ".txt" } },
                });

                    PickOptions options = new()
                    {
                        PickerTitle = "Please select a file to save the work ",
                        FileTypes = customFileType,

                    };

                    var result = await FilePicker.Default.PickAsync(options);
                    if (result != null)
                    {
                        if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                            result.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase))
                        {
                            savedFiles = result.FullPath;
                        }
                        SaveWork(savedFiles);
                    }
                }
            }

        }

        Application.Current.CloseWindow(Application.Current.MainPage.Window);
    }

    private async void mnuConvert_Clicked(object sender, EventArgs e)
    {
        try
        {
            var sourceText = txtSourceText.Text;
            var font = (lstFont.SelectedItem as MalayalamFont).Name;

            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont(font);

            var convertedText = MalayalamConverter.Core.Converter.ConvertAsciiToMalayalamUnicode(sourceText, mapping);

            txtDesinationText.Text = convertedText;

            isDirty = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", $"Filed to convert: {ex.Message}", "OK");
        }
    }

    private async void mnuSave_Clicked(object sender, EventArgs e)
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".txt" } },
                 });

            PickOptions options = new()
            {
                PickerTitle = "Please select a file to save the unicode ",
                FileTypes = customFileType,

            };



            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase))
                {
                    var fileInfo = new FileInfo(result.FullPath);
                    if (!fileInfo.Exists)
                    {
                        fileInfo.Create();
                    }

                    using var stream = fileInfo.OpenWrite();
                    using var streamWriter = new StreamWriter(stream);
                    await streamWriter.WriteLineAsync(txtDesinationText.Text);

                    await DisplayAlert("Save successful", "Malayalam unicode is now saved", "Ok", FlowDirection.LeftToRight);
                }
            }

            isDirty = false;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", $"Filed to load file: {ex.Message}", "OK");
        }
    }

    private async void mnuSaveWork_Clicked(object sender, EventArgs e)
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".txt" } },
                 });

            PickOptions options = new()
            {
                PickerTitle = "Please select a file to save the work ",
                FileTypes = customFileType,

            };

            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase))
                {
                    savedFiles = result.FullPath;
                }
            }

            if (string.IsNullOrEmpty(savedFiles))
            {
                SaveWork(savedFiles);
                await DisplayAlert("Save successful", "Work is now saved", "Ok", FlowDirection.LeftToRight);
                isDirty = false;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", $"Filed to save the work file: {ex.Message}", "OK");
        }
    }

    private async void mnuLoadWork_Clicked(object sender, EventArgs e)
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".txt" } },
                 });

            PickOptions options = new()
            {
                PickerTitle = "Please select the work file",
                FileTypes = customFileType,
            };


            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("doc", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    using (var streamReader = new StreamReader(stream))
                    {
                        var data = await streamReader.ReadToEndAsync();
                        var jObject = JsonConvert.DeserializeObject<JObject>(data);
                        if (!jObject.ContainsKey("ascii") || !jObject.ContainsKey("unicode"))
                        {
                            await DisplayAlert("Alert", $"Incorrect work file", "OK");
                        }
                        else
                        {
                            txtSourceText.Text = jObject.Value<string>("ascii");
                            txtDesinationText.Text = jObject.Value<string>("unicode");
                            await DisplayAlert("Loaded Succfully", $"Work file has been loaded sucessfully", "OK");
                        }
                    }
                }

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", $"Filed to load file: {ex.Message}", "OK");
        }
    }

    private async void SaveWork(string filePath)
    {
        var jObject = new JObject();
        jObject["ascii"] = txtSourceText.Text;
        jObject["unicode"] = txtDesinationText.Text;

        var text = JsonConvert.SerializeObject(jObject);

        var fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists)
        {
            fileInfo.Create();
        }

        using var stream = fileInfo.OpenWrite();
        using var streamWriter = new StreamWriter(stream);
        await streamWriter.WriteLineAsync(text);
    }

}