# maui-localize-test

Improved version of default Maui dot net bot waving hi to support localization.

 - Localized in English (US), French, German and Chinese (Simplified)
   - Localized strings moved to Resources/Strings/AppStrings
   - AppStrings.[name] used to retrieve the localized string
 - Uses MVVM pattern with code-behind moved to respective view models
 - Uses WeakReferenceMessenger to broadcast and receive locale changes
 - Uses Shell navigation to move between the pages
 - Uses Binding to update localize text when locale changes
