using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Maui.Localization;

public static class LocalizationAppBuilderExtension
{
    public static MauiAppBuilder UseToolkitMauiLocalization(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LocalizationManager>();
        return builder;
    }
}
