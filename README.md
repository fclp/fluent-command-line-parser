fluent-command-line-parser
==========================
A simple, strongly typed .NET C# command line parser library using a fluent easy to use interface

***

Commands such as `updaterecord.exe /r 10 /v="Mr. Smith" /silent` can be captured using

````var parser = new FluentCommandLineParser();
   
parser.Setup<int>("r").Callback(record => RecordID = record).Required();
parser.Setup<string>("v").Callback(value => NewValue = value).Required();
parser.Setup<bool>("s", "silent").Callback(silent => InSilentMode = silent).SetDefault(false);
   
parser.Parse(args);```

### Parser Option Methods
Setup an option using a short name, or short and long name.

```
.Setup<int>("r")
.Setup<int>("r", "record")
```

`.Required()` Indicate the option is required and an error should be raised if it is not provided.

`.Callback(val => Value = val)` Provide a delegate to call after the option has been parsed

`.SetDefault(int.MaxValue)` Define a default value that is assigned to the callback if the option what not specified in the args

`.WithDescription("Execute operation in silent mode without feedback")` Give the option a description to be used when help text is generated e.g using -? or --help.


Supported syntax

`[-|--|/][switch_name][=|:| ][value]`

Also supports boolean names

`updaterecord.exe -s // enabled`
`updaterecord.exe -s- // disabled`
`updaterecord.exe -s+ // enabled`