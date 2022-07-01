# ASP.NET Core Sample

This sample demonstrates using Dotfuscator on ASP.NET Core applications.
The application uses a basic web service with an HTML UI and functions as a basic "echo" server and client.

## Building the Sample

Open the Developer Command Prompt for Visual Studio and navigate to the sample directory.

To build the project enter the commands:

    dotnet restore
    msbuild EchoApp.csproj /p:Configuration=Release /t:Publish

The published app is placed in `bin\Release\net6.0\publish`.

## Running the Sample

Run the sample by opening a command prompt in `bin\Release\net6.0\publish` and enter:

    dotnet EchoApp.dll

You should see:

    Hosting environment: Production
    Content root path: $(PathToFolder)\bin\Release\net6.0\publish
    Now listening on: http://localhost:5050
    Application started. Press Ctrl+C to shut down.

Where `$(PathToFolder)` is where the `asp.netcore` folder is located.

Note that if the default listening address isn't available, it can be changed in the `Program.cs` file by replacing `.UseUrls("http://localhost:5050")` to the one of your choosing.
You will need to shut down the running sample, build again, and then start it again for the changes to be picked up.

## Testing the web page

Open a web browser and navigate to the address specified by *Now listening on* when the app started.
This brings up a test page for the app.
Ensure the *WebSocket Server URL* field is set to `ws://localhost:5050/ws` (or the appropriate address if you changed the listening address) and click *Connect*.
You should see *Ready to Connect…* change to *Open*.

Once *Open* is displayed, type in a message and click *Send* to see it displayed in the communication log.
The log shows that the client sent the message to the server and the server echoed it back to the client.
Once finished press the *Close Socket* button to end the connection.

Once done testing the sample, shut down the service by typing Ctrl+C in the command prompt.

## Verifying Obfuscation

To review the obfuscation applied to the `EchoApp.dll` file follow the instructions on the [Reverse Engineering](https://www.preemptive.com/dotfuscator/pro/userguide/en/protection_reverse_engineering.html) page.

## Build with Azure Pipelines

The project is built and protected by Dotfuscator Professional on Azure Pipelines. Check out the [azure-pipelines.yml](https://github.com/preemptive/dotfuscator-pro-samples/blob/master/asp.netcore/azure-pipelines.yml) file to see how it is configured and head to the [Dotfuscator Professional User Guide](https://www.preemptive.com/dotfuscator/pro/userguide/en/installation_build_agents.html) for additional details. 
