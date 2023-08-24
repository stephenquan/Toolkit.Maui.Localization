# maui-localize-test

The steps required to localize an app are:
 1. Create string Resource files with the convention:
      Resources/Strings/filename.resx
      Resources/Strings/filename.[additionalLanguage1].resx
      Resources/Strings/filename.[additionalLanguage2].resx
      ...
 2. Consume the resource strings in your app
 3. Optional set CultureInfo.CurrentUICulture and reload resource stings

To demonstrate, the following are localized versions of the MAUI starter app:

# maui-localize-resx

Demonstrates how to refer to the resource strings directly in the XAML.
However, in the code behind, we react to changes in language and rewrite all text fields.

# maui-localize-mvvm

Implements localization using MVVM.
Resource strings are exposed via getter functions in the ViewModel.
Those strings are linked to controls via the Binding markup extension in XAML.
Changes in language triggers OnPropertyChanged causing the getter functions to be reevaluated.
Uses CommunityToolkit.Mvvm.

# maui-localize-ext

Implements localization using both MVVM and the Microsoft Localization extension.
Resource strings are exposed via an IStringLocalizer in the ViewModel.
Bindings are directly with the IStringLocalizer and exposed in XAML.
Changes in language triggers OnPropertyChanged on the IStringLocalizer causing the strings to be reevaluated.
Uses CommunityToolkit.Mvvm and Microsoft.Extensions.Localization.

# maui-localize-lm

Implements a LocalizationManager which blackboxes both the IStringLocalizer and language changes.
Property binding is on both LocalizationManager "Culture" and "Item".
Changes in language will trigger OnPropertyChanged on "Item" causing the strings to be reevaluated.
Uses CommunityToolkit.Mvvm and Microsoft.Extensions.Localization.
