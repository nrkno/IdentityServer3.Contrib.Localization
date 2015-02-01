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
 * English (the default provided by the DefaultLocalizationService)
 * nb-NO (Norwegian Bokmål)
 * tr-TR (Turkish)
 * pirate (yarr)
 
## Install

```
  PM> Install-Package IdentityServer3.Localization
```

NuGet:
https://www.nuget.org/packages/IdentityServer3.Localization


## Dependencies

 * Thinktecture.IdentityServer3 - http://www.nuget.org/packages/Thinktecture.IdentityServer3/
