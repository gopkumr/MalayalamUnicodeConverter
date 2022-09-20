using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using System.Drawing.Text;
using Microsoft.UI.Xaml.Controls;
using System;
using static System.Collections.Specialized.BitVector32;
using System.Text;
using System.Data;

namespace MalayalamConverter.UI.Views;

public partial class MalayalamType : ContentPage
{
    public MalayalamType()
    {
        InitializeComponent();

        var fonts = new InstalledFontCollection();
        var installedFonts = fonts.Families.Select(q => new { Name = q.Name }).ToList();
        BindingContext = new { Fonts = installedFonts };
    }

    string processedWord = string.Empty;
    string typedWord = string.Empty;
    private async void Editor_TextChanged(object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var position = txtTextArea.CursorPosition;
        if (e.OldTextValue != null || e.NewTextValue != null)
        {
            var typedKey = e.NewTextValue;
            if (e.NewTextValue.Length >= position && !string.IsNullOrEmpty(e.NewTextValue))
            {
                typedWord = e.NewTextValue.Substring(0, position).Split(" ", StringSplitOptions.RemoveEmptyEntries).Last();
                typedKey = e.NewTextValue.ElementAt(position - 1).ToString();
            }

            if (e.NewTextValue != null && !string.IsNullOrEmpty(e.NewTextValue) && typedKey == " ")
            {
                var lastword = typedWord;
                typedWord=string.Empty;

                if (lastword != processedWord)
                {
                    spinner.IsRunning = true;
                    var url = $"https://inputtools.google.com/request?text={lastword}&itc=ml-t-i0-und&num=5&cp=0&cs=1&ie=utf-8&oe=utf-8";
                    HttpClient client = new HttpClient();
                    var stringResponse = await client.GetStringAsync(url);
                    var response = JsonConvert.DeserializeObject<JArray>(stringResponse);

                    if (response.Count > 0)
                    {
                        var isSuccess = response.First().Value<string>();
                        if (isSuccess.ToUpper().Equals("SUCCESS"))
                        {

                            var innerData = response.Skip(1).First().Value<JArray>().First().Value<JArray>();
                            var word = innerData.First().Value<string>();
                            var suggestions = innerData.Skip(1).First().Value<JArray>();

                            var options = suggestions.Select(q => q.Value<string>()).ToArray();
                            var mapping = MalayalamConverter.Core.MalayalamFonts.GetMapContentForFont("ML-Karthika-Normal");

                            if (toggleSuggestions.IsToggled)
                            {
                                string action = await DisplayActionSheet($"Suggestions for {word}", "Cancel", null, options);
                                if (action != null && action != "Cancel")
                                {
                                    var ascii = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(action, mapping);
                                    processedWord = ascii;
                                    txtTextArea.UpdateText(txtTextArea.Text.Replace(lastword, ascii));

                                }
                            }
                            else
                            {
                                
                                var ascii = MalayalamConverter.Core.Converter.ConvertMalayalamUnicodeToAscii(options.First(), mapping);
                                txtTextArea.UpdateText(txtTextArea.Text.Replace(lastword, ascii));
                                processedWord = ascii;
                            }
                        }
                    }
                }
            }
            spinner.IsRunning = false;
        }
    }

    private void lstFont_SelectedIndexChanged(object sender, EventArgs e)
    {
        string fontFamily = (lstFont.SelectedItem as dynamic).Name;
        txtTextArea.FontFamily = fontFamily;
    }
}