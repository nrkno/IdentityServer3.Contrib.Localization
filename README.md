master:[![master](https://ci.appveyor.com/api/projects/status/63g2yulmxod35vd1/branch/master?svg=true)](https://ci.appveyor.com/project/JohnKorsnes/identityserver3-contrib-localization/branch/master)
dev:[![dev](https://ci.appveyor.com/api/projects/status/63g2yulmxod35vd1/branch/dev?svg=true)](https://ci.appveyor.com/project/JohnKorsnes/identityserver3-contrib-localization/branch/dev)
[![NuGet Stable](http://img.shields.io/nuget/v/IdentityServer3.Localization.svg?style=flat)](https://www.nuget.org/packages/IdentityServer3.Localization/)
[![Downloads](https://img.shields.io/nuget/dt/IdentityServer3.Localization.svg)](https://www.nuget.org/packages/IdentityServer3.Localization/)

# Contents

Implementation of IdentityServerV3s ILocalizationService


## Usage

Specific culture:
```
   var options = new LocaleOptions { Locale = "nb-NO" };
   var localizationService = new GlobalizedLocalizationService(options);
```

To use IdentityServer3s default provided localization:
```
   var localizationService = new GlobalizedLocalizationService();
```

Pirate culture:
```
   var options = new LocaleOptions { Locale = "pirate" }; // ye be warned!
   var localizationService = new GlobalizedLocalizationService(options);
```


## Supported languages
 * [Default/English (the default provided by the DefaultLocalizationService)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/Default)
 * [da-DK (Danish)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/da-DK), Takk [Dalager](https://github.com/Dalager)!  
 * [de-DE (German)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/de-DE), danke Schön [ManuelRauber](https://github.com/ManuelRauber)!
 * [es-AR (Spanish, Argentina)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/es-AR), ¡Muchas gracias, [gustavoruscitto](https://github.com/gustavoruscitto)!
 * [fr-FR (French)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/fr-FR), Merci, [ghys](https://github.com/ghys)!
 * [nb-NO (Norwegian Bokmål)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/nb-NO)
 * [pt-BR (Portuguese, Brazil)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/ro-RO), obrigado, [julianpaulozzi ](https://github.com/julianpaulozzi)!
 * [ro-RO (Romanian)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/ro-RO), vă mulțumesc, [totpero](https://github.com/totpero)!
 * [ru-RU (Russian)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/ru-RU), Спасибо, [ka3eu](https://github.com/ka3eu)!
 * [sv-SE (Swedish)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/de-DE), tack [krippz](https://github.com/krippz)!
 * [tr-TR (Turkish)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/tr-TR), [Iltera](https://github.com/iltera) sayesinde!
 * [zh-CN (Chinese, Traditional)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/zh-CN), .. not even going to try ;) Thanks, [heavenwing](https://github.com/heavenwing) and [yuxiaochou](https://github.com/yuxiaochou) !
 * [pirate (yarr)](http://johnkors.github.io/IdentityServer3.Contrib.Localization/#/pirate), yarr repo owner!

## Install

```
  PM> Install-Package IdentityServer3.Localization
```

NuGet:
https://www.nuget.org/packages/IdentityServer3.Localization


## Contributing

How to add another language:

 * Fork the repo
 * Add the following resource files for your language in the resource folder (for instance by copying the default). ISO codes can be found [here])https://msdn.microsoft.com/en-us/library/ee796272(v=cs.20).aspx)

  1. Events.ISO-code-for-your-translation.resx
  2. Messages.ISO-code-for-your-translation.resx
   3. Scopes.ISO-code-for-your-translation.resx

 * Run the tests and fix any errors so they are green!
 * Rebase off upstream if behind, and submit the Pull Request




## Dependencies

 * Thinktecture.IdentityServer3 - http://www.nuget.org/packages/Thinktecture.IdentityServer3/
