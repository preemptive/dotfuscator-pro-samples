# PreEmptive Protection - Dotfuscator Professional Samples

This repo contains samples that demonstrate configuring [PreEmptive Protection - Dotfuscator Professional](https://www.preemptive.com/products/dotfuscator/overview) for various types of .NET applications. 
The samples in this repo are each organized into their own directory.
For sample-specific instructions, refer to the the README in each sample's directory.

## Samples in this repo

The following samples are available in this repo:
* [GettingStarted](GettingStarted) accompanies the [Protect Your App](https://www.preemptive.com/dotfuscator/pro/userguide/en/getting_started_protect.html) page in the [Dotfuscator User Guide](https://www.preemptive.com/dotfuscator/pro/userguide/en/index.html) and is useful if you are looking to quickly protect your Visual Studio project with Dotfuscator.
* [ASP.NET Core](asp.netcore/) demonstrates using Dotfuscator on ASP.NET Core applications.
* [Reflection](reflection) uses renaming rules to fix issues that occur when using Dotfuscator with applications that make use of dynamic class loading and method invocation.
* [Serialization](serialization) demonstrates using Dotfuscator in an application that makes use of serialized objects that must be exchanged with non-obfuscated code.

This repo has tags that correspond to the earliest Dotfuscator Professional version number the samples are compatible with. 

## Samples in other repos

### Xamarin 

Samples for integrating and configuring Dotfuscator protection for Xamarin apps can be found in the following repos:
* [protected-bugsweeper](https://github.com/preemptive/protected-bugsweeper) demonstrates integrating Dotfuscator into the build process for a Xamarin app and then enhancing the protection applied by Dotfuscator.
This sample demonstrates protecting a Xamarin app with Tamper Checks and responding to a tampered Android app.
* [protected-TodoAzureAuth](https://github.com/preemptive/Protected-TodoAzureAuth) accompanies [Detect and Respond to Rooted Android Devices from Xamarin Apps](https://msdn.microsoft.com/en-US/magazine/mt846653). 
This sample demonstrates protecting a Xamarin app with Root Checks and the different methods for responding to a rooted Android device.

### Checks

Samples for protecting applications with [Checks](https://www.preemptive.com/dotfuscator/pro/userguide/en/protection_checks_overview.html) can be found in the following repos:
* [protected-adventureworks](https://github.com/preemptive/protected-adventureworks) provides an example of protecting a WPF app with Dotfuscator's Runtime Checks
* [dot-check-sample](https://github.com/preemptive/dot-check-sample) is a WPF application designed to demonstrate use cases and patterns for Dotfuscator's [anti-debug protections](https://www.preemptive.com/dotfuscator/pro/userguide/en/protection_checks_debug.html)

