Fluent Command Line Parser
==========================
A simple, strongly typed .NET C# command line parser library using a fluent easy to use interface.

### Download

You can download the latest release from [CodeBetter's TeamCity server](http://teamcity.codebetter.com/project.html?projectId=project314)

You can also install using [NuGet](http://nuget.org/packages/FluentCommandLineParser/)

```
PM> Install-Package FluentCommandLineParser
```
Commands such as `updaterecord.exe /r 10 /v="Mr. Smith" /silent` can be captured using

```
static void Main(string[] args)
{
  IFluentCommandLineParser parser = new FluentCommandLineParser();

  parser.Setup<int>("r")
		.Callback(record => RecordID = record)
		.Required();

  parser.Setup<string>("v")
		.Callback(value => NewValue = value)
		.Required();

  parser.Setup<bool>("s", "silent")
		.Callback(silent => InSilentMode = silent)
		.SetDefault(false);

  parser.Parse(args);
}
```
### Parser Option Methods

`.Setup<int>("r")` Setup an option using a short name, 

`.Setup<int>("r", "record")` or short and long name.

`.Required()` Indicate the option is required and an error should be raised if it is not provided.

`.Callback(val => Value = val)` Provide a delegate to call after the option has been parsed

`.SetDefault(int.MaxValue)` Define a default value if the option was not specified in the args

`.WithDescription("Execute operation in silent mode without feedback")` Specify a help description for the option

### Setup Help

Setup to print the available options to the console when any of the help args are found.

```	
static void Main(string[] args)
{
   var parser = new FluentCommandLineParser();

   parser.SetupHelp("h", "help", "?")
         .Callback(Console.WriteLine);

   var result = parser.Parse(args);
   
   if(result.HelpCalled) return;
}
```
### Supported syntax

`[-|--|/][switch_name][=|:| ][value]`

Also supports boolean names

```
updaterecord.exe -s // enabled
updaterecord.exe -s- // disabled
updaterecord.exe -s+ // enabled
```

### Development

Fclp is in the early stages of development. Please feel free to provide any feedback on feature support or the Api itself.

If you would like to contribute, you may do so to the [develop branch](https://github.com/fclp/fluent-command-line-parser/tree/develop).