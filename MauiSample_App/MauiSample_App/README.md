
## About RRE-1211 Tickect

Description
The MAUI Sample App is missing from our GitHub repository: [https://github.com/preemptive/dotfuscator-pro-samples/tree/master/MauiApp1 ] This was previously available, but the link now yields a 404 error.

## MAUI App Sample - Homepage to TabbedPage

This is a sample .NET MAUI application that demonstrates basic navigation from a homepage to a `TabbedPage`. The `TabbedPage` contains three tabs, each displaying different content. This sample is intended to provide a starting point for developers building mobile applications with .NET MAUI.

This .NET MAUI application demonstrates how to use resource files within a MAUI app. The app includes examples of loading string data, image data, and using properties and methods from resource files. Additionally, this README provides important information regarding the risks of reverse engineering the application.


## Building the MAUI APP Sample

The MAUI APP sample can be built from Visual Studio or by running `msbuild` in the sample's directory. When built in Release mode (i.e. passing `/p:Configuration=Release` to `msbuild`), the application will be processed by Dotfuscator as part of the build.

## Features


- **Properties,String and Methods:** Example of using Strings and methods that binds the data to Lables in TabbedPageDemo.Xaml.
- **Security Considerations:** Discusses the potential risks of reverse engineering .NET assemblies using tools like ILSpy and dnSpy.

## Requirements

- **.NET 6.0** or higher
- **Visual Studio 2022** with .NET MAUI workload installed
- **Windows 10/11** or **macOS** for development

## Getting Started


Build and Run the Project
Open the Solution:

Open MauiAppSample.sln in Visual Studio 2022.

Restore NuGet Packages:

Visual Studio should automatically restore the necessary NuGet packages.

Deploy the App:

Select the target platform (Android, iOS, Windows) from the toolbar.
Click the Run button to deploy the app to your selected device or emulator.

Project Structure
MainPage.xaml: The homepage containing a button that navigates to TabbedPageDemo.
TabbedPageDemo.xaml: A TabbedPage with three tabs(Tab1,Tab2 and Tab3).
Tab1: Display the content for the First Tab.
Tab2: Display the content for the Second Tab.
Tab3: Display the content for the Third Tab.

## Code Overview

## Home Page (MainPage.xaml)
```
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiApp3.MainPage">

    <ScrollView>
        <VerticalStackLayout
            Padding="30,0"
            Spacing="25">
            <Image
                Source="dotnet_bot.png"
                HeightRequest="185"
                Aspect="AspectFit"
                SemanticProperties.Description="dot net bot in a race car number eight" />

            <Label
                Text="Hello, World!"
                Style="{StaticResource Headline}"
                SemanticProperties.HeadingLevel="Level1" />

            <Label
                Text="Welcome to &#10;.NET Multi-platform App UI"
                Style="{StaticResource SubHeadline}"
                SemanticProperties.HeadingLevel="Level2"
                SemanticProperties.Description="Welcome to dot net Multi platform App U I" />

            <Button
                x:Name="CounterBtn"
                Text="Click me" 
                SemanticProperties.Hint="Counts the number of times you click"
                Clicked="OnCounterClicked"
                HorizontalOptions="Fill" />
        </VerticalStackLayout>
    </ScrollView>

</ContentPage>
```

## Button Click Event (MainPage.xaml.cs)
```
namespace MauiSample_App
{
    public partial class MainPage : ContentPage
    {
  

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabbedPageDemo());
            

        }
    }

}
```
## App.Xaml 

```
<?xml version = "1.0" encoding = "UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:MauiApp3"
             x:Class="MauiSample_App.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```
## App.Xaml.cs

```
namespace MauiApp3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           // MainPage = new AppShell();
            var NavPage = new NavigationPage(new MainPage());
            NavPage.BarBackground = Colors.Green;
            NavPage.BarTextColor = Colors.White;
            MainPage = NavPage;
        }
    }
}
```
## TabbedPageDemo(TabbedPageDemo.Xaml)
```
<?xml version="1.0" encoding="utf-8" ?>
<TabbedPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiSample_App.TabbedPageDemo"
             Title="TabbedPageDemo"
            BarBackground="Green"
            BarTextColor="white"
            SelectedTabColor="Red"
            UnselectedTabColor="Orange">
    <ContentPage Title="Tab1" IconImageSource="dotnet_bot.png">
        <Label Text="This is Tab 1 " x:Name="lblT1"></Label>
    </ContentPage>
    <ContentPage Title="Tab2" IconImageSource="dotnet_bot.png">
        <Label Text="This is Tab 2 " x:Name="lblT2"></Label>
    </ContentPage>
    <ContentPage Title="Tab3" IconImageSource="dotnet_bot.png">
        <Label Text="This is Tab 3 " x:Name="lblT3"></Label>
    </ContentPage>

</TabbedPage>

```
## TabbedPageDemo(TabbedPageDemo.Xaml.cs)

```
namespace MauiSample_App;

public partial class TabbedPageDemo : TabbedPage
{
	public TabbedPageDemo()
	{
		InitializeComponent();
        BindLabel();


    }
    private void BindLabel()
    {
        string strTab1 = "Demo Tabbed Page Demo1";
        string strTab2 = "Demo Tabbed Page Demo2";
        string strTab3 = "Demo Tabbed Page Demo3";
    
    var binding = new Binding
    {
        Source = strTab1.ToString()
    };

    lblT1.SetBinding(Label.TextProperty, binding);

    var binding1 = new Binding
    {
        Source = strTab2.ToString()
    };

    lblT2.SetBinding(Label.TextProperty, binding1);

    var binding2 = new Binding
    {
        Source = strTab3.ToString()
    };

    lblT3.SetBinding(Label.TextProperty, binding2);
}
    

}
```

## Configuring the Resource Encryption Sample with the Config Editor

The Dotfuscator Config Editor provides a visual means to produce a config file. Run the Dotfuscator Config Editor from the Start Menu and navigate to the `DotfuscatorConfig.xml` file via the File -> Open menu. First change the `Disable string encryption` fetaure from `Yes` to `No` under the settings tab options section: 



![](Resources/images/settings.png)

Expanding the assembly node in the tree shows a graphical view of the application structure, including all resource properties and select the properties to encrypt:

![](Resources/images/string_encryption.png)

Once you've saved your changes to the config file (e.g., with the File -> Save command in the Config Editor), you need to build your app with the new settings, using your normal build process (Visual Studio, MSBuild, etc.).


## Summary of the Resource Enryption Sample

In order for you to successfully obfuscate an application that have string to be encrypt, change the settings of `Disable string Encryption` fetaure from `Yes` to `No` and select the appropriate page/strings from the string Encryption tab. Dotfuscator provides a fine-grained, rule based facility for doing this.
