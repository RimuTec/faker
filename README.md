# Faker
RimuTec.Faker provides generators for fake, yet realistically looking data. Use it for testing, for creating screenshots to show off your cool software, and similar more. Generators include Lorem, Name, Address, Date, Company, Business, and similar more.

RimuTec.Faker is a C# port of the Ruby Faker gem [https://github.com/faker-ruby/faker](https://github.com/faker-ruby/faker). It also has some generators and methods that are not in the original Ruby Faker, e.g. Weather.

RimuTec.Faker supports the following targets:
- .NET Standard 2.0 (netstandard2.0)
- .NET Framework 4.6.2 (net462)

In case you need support for other targets, please file an issue at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues) and we'll check if we can also support the target you need. Thank you!

The library including its source code are licensed under the MIT license. It supports 51 locales out of the box. And you can extend it with your own custom locales using yaml files.

| Metric      | Status      |
| ----- | ----- |
| Nuget       | [![NuGet Badge](https://buildstats.info/nuget/RimuTec.Faker)](https://www.nuget.org/packages/RimuTec.Faker/) |

# Usage

## Quick Start
1. Install NuGet package. See [https://www.nuget.org/packages/RimuTec.Faker](https://www.nuget.org/packages/RimuTec.Faker) for instructions
2. Add `using RimuTec.Faker;` at the beginning of your C# source file (or the equivalent for your preferred .NET language), e.g.
   ```csharp
   using RimuTec.Faker; // at the beginning of your file
   ```
3. Generate fake data in your tests, e.g. 
   ```csharp
   var firstName = Name.NameWithMiddle(); // in your test
   ```
   or 
   ```csharp
   var paragraphs = Lorem.Paragraphs(4); // in your test
   ```

In case of name clashes with other classes in your code base, use one of [these techniques](https://github.com/RimuTec/Faker/wiki/Name-Clashes).

## Documentation
We also have more detailed [documentation for RimuTec.Faker](https://rimutec.github.io/Faker/).

## Release Notes
Release notes are available at [https://github.com/RimuTec/Faker/blob/master/releasenotes.md](https://github.com/RimuTec/Faker/blob/master/releasenotes.md)

## Support & Suggestions
If you need support or have a suggestion for improvement please file an issue at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues). Thank you!

## Semantic Versioning

We use [semantic versioning (SemVer)](https://semver.org/) for RimuTec.Faker. This means we endeavor to follow these rules:

Given a version number `MAJOR.MINOR.PATH` we increment:
1. `MAJOR` version if we make incompatible API changes, i.e. breaking changes
2. `MINOR` version if we make compatible API changes, i.e. we add functionality in a backwards compatible manner
3. `PATCH` version if we make backwards compatible bug fixes but don't add new functionality

## Reporting Bugs
RimuTec.Faker has a test suite with over 300 unit tests. This does not guarantee absence of bugs. Please report all bugs at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues) ideally including steps to reproduce. We also consider pull requests (PR). All your feedback will help make the library more valuable for other users as well. Thank you!

# Available Fake Data Generators
The classes listed below are already ported. Our aim is to add the remaining classes and method over time. If you have preferences please file suggestions as issues on Github (see below). Thank you!

- [Color](https://rimutec.github.io/Faker/Color/index.md)
- [Address](https://github.com/RimuTec/Faker/wiki/Class-Address)
- [Business](https://github.com/RimuTec/Faker/wiki/Class-Business)
- [Code](https://github.com/RimuTec/Faker/wiki/Class-Code)
- [Company](https://github.com/RimuTec/Faker/wiki/Class-Company)
- [Date](https://github.com/RimuTec/Faker/wiki/Class-Date)
- [Educator](https://github.com/RimuTec/Faker/wiki/Class-Educator)
- [Finance](https://github.com/RimuTec/Faker/wiki/Class-Finance)
- [IdNumber](https://github.com/RimuTec/Faker/wiki/Class-IdNumber)
- [Internet](https://github.com/RimuTec/Faker/wiki/Class-Internet)
- [Job](https://github.com/RimuTec/Faker/wiki/Class-Job)
- [Lorem](https://github.com/RimuTec/Faker/wiki/Class-Lorem)
- [Name](https://github.com/RimuTec/Faker/wiki/Class-Name)
- [PhoneNumber](https://github.com/RimuTec/Faker/wiki/Class-PhoneNumber)
- [RandomNumber](https://github.com/RimuTec/Faker/wiki/Class-RandomNumber)
- [Weather](https://github.com/RimuTec/Faker/wiki/Class-Weather)

Class to set the locale to be used:
- [Config](https://github.com/RimuTec/Faker/wiki/Class-Config)

# Locales and Customization
The following 51 locales are supported out of the box (no extra files needed):

bg, ca, ca-CAT, da-DK, de, de-AT, de-CH, ee, en, en-AU, en-au-ocker, en-BORK, en-CA, en-GB, en-IND, en-MS, en-NEP, en-NG, en-NZ, en-PAK, en-SG, en-UG, en-US, en-ZA, es, es-MX, fa, fi-FI, fr, fr-CA, fr-CH, he, id, it, ja, ko, lv, nb-NO, nl, no, pl, pt, pt-BR, ru, sk, sv, tr, uk, vi, zh-CN, zh-TW

To set the locale in C# use something like `Config.Locale = "de";`.

In addition you can use custom locale files for methods that are marked with an asterisk. Ensure that the custom locale file (yml) is copied to the directory that also contains RimuTec.Faker.dll, usually the output directory of your test project.

# How To Build
## Visual Studio 2019
Open `Faker.sln` in Visual Studio, select the desired configuration ("Debug" or "Release") and then build the solution. Typically, you'd choose "Debug" when working on this code base.

Note: We use Visual Studio 2019 Community Edition. Other versions and editions may work as well but we didn't test them.

## Command Line
1. Open Powershell and navigate to the directory containing `Faker.sln`
2. Execute the command `dotnet build --configuration RELEASE Faker.sln`. Replace RELEASE with DEBUG if you want build the DEBUG configuration

## Issues With Building
If you encounter issues with building the library please file an issue on GitHub, ideally with what you tried, what the expected and what the actual outcome was. Thank you!

# Credits
This project uses yaml source files from the [Ruby Faker gem](https://github.com/faker-ruby/faker), licensed under the MIT license. Thank you to all their contributors!
