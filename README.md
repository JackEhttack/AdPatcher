## Preface
***By using this tool, you are directly depriving the developers of the
software you are applying this tool to of income and motivation to continue
development of their (presumably) awesome software.***

In addition, this almost certainly violates the ToS of the software you are
using. Use at your own peril! 

## Requirements
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## What does this do?
This is a simple tool that unpacks Electron apps, removes all instances of the
`<owadview>` tag, and repacks the app.

I was previously manually doing this, but I figured it was a simple enough task
to do programmatically and a good excuse to learn how to make desktop C# 
applications.

## Potential Issues
If after applying the tool you find yourself unable to launch the app you can
revert the changes by navigating to the folder where `app.asar` is located.

## Compatibilities:
The following is a list of known compatible applications:

| Application | Directory |
| ----------  | ------------------------------------------------------- |
| GDLauncher  | `%localappdata%/Programs/@gddesktop/resources/app.asar` |

Feel free to add to this list with an issue, PR, or send a carrier pidgeon my way!  

## Build
I am rather new to C# development, please take this with a grain of salt.

The source code requires an external library called [asarsharp](https://github.com/MWR1/asarsharp) 
and it's dependencies to compile. Link it to the project however you do in your
IDE of choice and compile away!

## Special Thanks
To MWR for granting me permission to use their asarsharp library!
