# Toolkit.Maui.Localization

## Introduction

The universal steps required to localize an app are:
 1. Create string Resource files with the convention:
      - Resources/Strings/filename.resx
      - Resources/Strings/filename.[additionalLanguage1].resx
      - Resources/Strings/filename.[additionalLanguage2].resx
 2. Consume the resource strings in your app
 3. Optional set CultureInfo.CurrentUICulture and reload resource stings

## Using Toolkit.Maui.Localization

Call `UseToolkitMauiLocalization` in your `MainProgram.cs` and nominate your default string resource:

```c#
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    .UseToolkitMauiLocalization<AppStrings>()
```

## Obtain LocalizationManager singleton via constructor injection

```c#
public partial class MainPage : ContentPage
{
    public LocalizationManager LM { get; }
    public MainPage(LocalizationManager LM)
    {
        this.LM = LM;
    }
}
```

## For ease of use in XAML, recommend i18n namespace

```xaml
<ContentPage xmlns:i18n="clr-namespace:Toolkit.Maui.Localization;assembly=Toolkit.Maui.Localization">
```

## Localize markup extension

```xaml
<!-- LBL_HELLO is a string resource containing "Hello, World!" -->
<Label Text="{i18n:Localize LBL_HELLO}"/>
```

## PluralConverter

```xaml
    <ContentPage.Resources>
        <ResourceDictionary>
            <i18n:PluralConverter x:Key="PluralConverter"/>
        </ResourceDictionary>
    </ContentPage.Resources>

    <Button
        x:Name="CounterBtn"
        Text="{MultiBinding
                   {Binding Count},
                   {i18n:Localize STR_CLICKED_N_TIMES},
                   {i18n:Localize STR_CLICKED_1_TIME},
                   {i18n:Localize STR_CLICK_ME},
                   Converter={x:StaticResource PluralConverter}}"
        SemanticProperties.Hint="Counts the number of times you click"
        Clicked="OnCounterClicked"
        HorizontalOptions="Center" />
```xaml
