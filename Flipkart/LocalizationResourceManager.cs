using System;
using System.ComponentModel;
using System.Globalization;
using Flipkart.Resources.Localization;

namespace Flipkart;

public class LocalizationResourceManager: INotifyPropertyChanged
{
    public LocalizationResourceManager()
    {
        // setting up current culture
        language.Culture = CultureInfo.CurrentCulture;
    }

    public static LocalizationResourceManager Instance {get; } = new();

    public object this[string resourceKey] => language.ResourceManager.GetObject(resourceKey, language.Culture) ?? Array.Empty<byte>();

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetCulture(CultureInfo culture)
    {
        language.Culture = culture;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
}
