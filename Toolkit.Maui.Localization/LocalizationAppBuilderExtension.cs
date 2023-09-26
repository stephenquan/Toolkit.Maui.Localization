using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Maui.Localization;

public static class LocalizationAppBuilderExtension
{
    public static MauiAppBuilder UseToolkitMauiLocalization<TStringResource>(this MauiAppBuilder builder)
    {
        LocalizationManager.DefaultStringResource = typeof(TStringResource);
        builder.Services.AddSingleton<LocalizationManager>();
        return builder;
    }
}
