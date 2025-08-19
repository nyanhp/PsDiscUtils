>This repo has moved to <https://codeberg.org/nyanhp/psdiscutils>
 
 # PsDiscUtils

PowerShell module that aims to implement the DiscUtils library.

## Installation and usage

To install the module, please use the PowerShell Gallery. At some point I'll also package a release,
but I hardly see the point.

```powershell
# Install
Install-Module -Repository PSGallery -Scope CurrentUser -Name PsDiscUtils # -AllowPrerelease to get the GOOD STUFF

# Explore
Get-Command -Module PsDiscUtils
```

## Build it yourself

Using the .NET Core SDK you can build that project your self. Why? Because I've gone
the easy route and implemented a bunch of C# cmdlets instead of writing PowerShell functions.

```powershell
./build/vsts-prerequisites.ps1
./build/vsts-build.ps1 -SkipPublish
```

## Requirements

- PowerShell 5 and newer
- An overwhelming urge to automate the crap out of things

## NuGet packages used

Licenses apply and so on.

- [PowerShellStandard.Library](https://github.com/PowerShell/PowerShellStandard)
- [DiscUtils](https://github.com/DiscUtils/DiscUtils)
