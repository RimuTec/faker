# Faker
RimuTec.Faker provides generators for fake, yet realistically looking data. Use it for testing, for creating screenshots to show off your cool software, and similar more. Generators include Lorem, Name, Address, Date, Company, Business, and similar more.

RimuTec.Faker is a C# port of the Ruby Faker gem [https://github.com/faker-ruby/faker](https://github.com/faker-ruby/faker). It also has some generators and methods that are not in the original Ruby Faker, e.g. Weather.

RimuTec.Faker supports the following targets:
- .NET Standard 2.0 (netstandard2.0)
- .NET Stnadard 2.1 (netstandard2.1)
- .NET Framework 4.6.2 (net462)
- .NET 5.0 (net5.0)

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
RimuTec.Faker has a test suite with over 13,000 unit tests covering all built-in locales. This does not guarantee absence of bugs.

If you find a bug please consider making a contribution to the open-source community by reporting the bug at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues). Ideally, please include steps to reproduce the issue. We also consider pull requests (PR). All your feedback will help make the library more valuable for other users as well. Thank you!

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
The following 50 locales are supported out of the box (no extra files needed):

bg, ca, ca-CAT, da-DK, de, de-AT, de-CH, ee, en, en-AU, en-au-ocker, en-BORK, en-CA, en-GB, en-IND, en-MS, en-NEP, en-NG, en-NZ, en-PAK, en-SG, en-UG, en-US, en-ZA, es, es-MX, fa, fi-FI, fr, fr-CA, fr-CH, he, id, it, ja, ko, lv, nb-NO, nl, pl, pt, pt-BR, ru, sk, sv, tr, uk, vi, zh-CN, zh-TW

To set the locale in C# use something like `Config.Locale = "de";`.

In addition you can use custom locale files for methods that are marked with an asterisk. Ensure that the custom locale file (yml) is copied to the directory that also contains RimuTec.Faker.dll, usually the output directory of your test project.

**Note:** With release 1.8 we removed support for locale 'no' as the language file has an incorrect format. Once this issue has been corrected at [Ruby Faker gem](https://github.com/faker-ruby/faker) we'll add it back in if all tests pass.

# How To Build
## Prerequisites
- Docker Desktop (Windows and MacOS)
- Docker Engine (Linux)
- VS Code
- Git client
- VS Code extension "Remove Development", version 0.20.0 or later

We are using a dev container to ensure better consistency across environments but also to avoid having to install development tools on the host. You may be able to build without a container but we don't officially support that setup.

## Visual Studio Code
1. Clone the repository into a linux file system.
   - **Important note for Windows user:** Use a WSL or WSL2 distro. Also, do not clone into a path starting with `/mnt` as this is likely to cause issues with VS Code and some extensions. The background is that file change messages are not reliably propagated as of writing when mounting a non-Linux file system into a dev container which is Linux based. For more details about this see article ["Docker Desktop on WSL2: The Problem with Mixing File Systems"](https://levelup.gitconnected.com/docker-desktop-on-wsl2-the-problem-with-mixing-file-systems-a8b5dcd79b22?sk=53d24e33a9f247fd626e3aa6959de7d4).

2. Navigate to the directory containing your local clone.
   - **Windows only:** Use a WSL or WSL2 terminal to navigate to it. Do not use `\\wsl$` via File Explorer.

3. Open the folder in VS Code using the command `code .` in the shell. When prompted re-open in the dev container

4. To build, open a terminal inside VS Code, ensure you are in `/app` and execute `dotnet build` or `dotnet test`.

## Issues With Building
If you encounter issues with building the library please file an issue on GitHub, ideally with what you tried, what the expected and what the actual outcome was. Thank you!

# Credits
This project uses yaml source files from the [Ruby Faker gem](https://github.com/faker-ruby/faker), licensed under the MIT license. Thank you to all their contributors!
