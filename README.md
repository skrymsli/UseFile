# UseFile

A simple program that holds files in use for a specified amount of time or until you press a key.  It is for testing file-in-use scenarios.

```
Usage:

  UseFile.exe <options> <file1> <file2> <file3> ...

Options:

  -t, --timeout    Number of seconds to hold the files in use
```

Example:
```
> .\UseFile.exe .\crap.txt
Press a key to stop using file...
```

If you try to do something with a file in use you get a message like this:

```
copy : The process cannot access the file 'C:\Users\Cody\Documents\GitHub\UseFile\UseFile\bin\Debug\crap.txt'
because it is being used by another process.
At line:1 char:1
+ copy .\UseFile.exe .\crap.txt
+ ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    + CategoryInfo          : NotSpecified: (:) [Copy-Item], IOException
    + FullyQualifiedErrorId : System.IO.IOException,Microsoft.PowerShell.Commands.CopyItemCommand
```
