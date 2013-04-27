Fluent Command Line Parser
==========================
A simple, strongly typed .NET C# command line parser library using a fluent easy to use interface.
### Download

See what's new in [v1.2](https://github.com/fclp/fluent-command-line-parser/wiki/Roadmap).

You can download the latest release from [CodeBetter's TeamCity server](http://teamcity.codebetter.com/project.html?projectId=project314)

You can also install using [NuGet](http://nuget.org/packages/FluentCommandLineParser/)
```
PM> Install-Package FluentCommandLineParser
```
### Usage
See [here](https://github.com/fclp/fluent-command-line-parser/wiki/So,-how-does-FCLP-compare-to-other-parsers%3F) for a side-by-side syntax comparison between other command line parsers.

Commands such as `updaterecord.exe -r 10 -v="Mr. Smith" --silent` can be captured using
```
static void Main(string[] args)
{
  var p = new FluentCommandLineParser();

  p.Setup<int>('r')
   .Callback(record => RecordID = record)
   .Required();

  p.Setup<string>('v')
   .Callback(value => NewValue = value)
   .Required();

  p.Setup<bool>('s', "silent")
   .Callback(silent => InSilentMode = silent)
   .SetDefault(false);

  p.Parse(args);
}
```
### Parser Option Methods

`.Setup<int>('r')` Setup an option using a short name, 

`.Setup<int>('r', "record")` or short and long name.

`.Required()` Indicate the option is required and an error should be raised if it is not provided.

`.Callback(val => Value = val)` Provide a delegate to call after the option has been parsed

`.SetDefault(int.MaxValue)` Define a default value if the option was not specified in the args

`.WithDescription("Execute operation in silent mode without feedback")` Specify a help description for the option

### Parsing Using Fluent Command Line Builder

Instead of assigning parsed values to variables you can use the Fluent Command Line Builder to automatically create a defined object type and setup individual Options for each strongly-typed property. Because the builder is simply a wrapper around the parser you can still use the Fluent Command Line Parser Api to define the behaviour for each Option.

The Fluent Command Line Builder can build a type and populate the properties with parsed values such as in the following example: 
```
public class ApplicationArguments
{
   public int RecordId { get; set; }
   public bool Silent { get; set; }
   public string NewValue { get; set; }
}

static void Main(string[] args)
{
   // create a builder for the ApplicationArguments type
   var b = new FluentCommandLineBuilder<ApplicationArguments>();

   // specify which property the value will be assigned too.
   b.Setup(arg => arg.RecordId)
    .As('r', "record") // define the short and long option name
    .Required(); // using the standard fluent Api to declare this Option as required.

   b.Setup(arg => arg.NewValue)
    .As('v', "value")
    .Required();

   b.Setup(arg => arg.Silent)
    .As('s', "silent")
    .SetDefault(false); // use the standard fluent Api to define a default value if non is specified in the arguments

   var result = b.Parse(args);

   if(result.HasErrors == false)
   {
      // use the instantiated ApplicationArguments object from the Object property on the builder.
      application.Run(b.Object);
   }
}
```

### Parsing To Collections

Many arguments can be collected as part of a list. Types supported are `string`, `int`, `double` and `bool`

For example arguments such as

`--filenames C:\file1.txt C:\file2.txt "C:\other file.txt"`

can be automatically parsed to a `List<string>` using
```
static void Main(string[] args)
{
   var p = new FluentCommandLineParser();

   var filenames = new List<string>();

   p.Setup<List<string>>('f', "filenames")
    .Callback(items => filenames = items);

   p.Parse(args);

   Console.WriteLine("Input file names:");

   foreach (var filename in filenames)
   {
      Console.WriteLine(filename);
   }
}
```
output:
```
Input file names
C:\file1.txt
C:\file2.txt
C:\other file.txt
```
### Supported Syntax
`[-|--|/][switch_name][=|:| ][value]`

Supports boolean names
```
example.exe -s  // enable
example.exe -s- // disabled
example.exe -s+ // enable
```
Supports combined (grouped) options
```
example.exe -xyz  // enable option x, y and z
example.exe -xyz- // disable option x, y and z
example.exe -xyz+ // enable option x, y and z
```
### Development
Fclp is in the early stages of development. Please feel free to provide any feedback on feature support or the Api itself.

If you would like to contribute, you may do so to the [develop branch](https://github.com/fclp/fluent-command-line-parser/tree/develop).

[![githalytics.com alpha](https://cruel-carlota.pagodabox.com/cbcae8086524a79bd8779e37b579a244 "githalytics.com")](http://githalytics.com/fclp/fluent-command-line-parser)