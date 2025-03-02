# Perjure

[![Build status](https://coshea.visualstudio.com/Perjure/_apis/build/status/Perjure)](https://coshea.visualstudio.com/Perjure/_build/latest?definitionId=-1)

+ **Perjure** is a small, light-weight console application that allows you to purge folders after the number of days you define. Perjure works well with job schedulers that run the program at fixed intervals.

+ **Perjure** là một ứng dụng bảng điều khiển nhỏ, nhẹ cho phép bạn xóa các thư mục sau số ngày bạn xác định. Perjure hoạt động tốt với các trình lập lịch công việc chạy chương trình theo những khoảng thời gian cố định.

## Installation

Perjure requires no installation and can be deployed via ```xcopy```.
Simply unzip the release, modify the ```Configuration.json``` file to fit your needs,
and run ```Perjure.exe``` from the command line.

## Usage

### Step 1

Configure the settings file. The ```Configuration.json``` file defines all of the
purge rules that will be executed.

```json
[
  {
    "DirectoryPath": "D:\\DirectoryWhoseFilesToPurge",
    "MatchPattern": "",
    "DaysToPurgeAfter": 30,
    "MinimumFilesToKeep": 1,
    "MaximumFileSizeInBytesToKeep": 209715200,
    "IncludeSubdirectories": false,
    "IncludeHiddenFiles": false,
    "DeleteEmptySubdirectories": true,
    "TimeComparison": "LastWrite"
  }
]
```

The MatchPattern property is a C# regular expression. This allows for advanced file matching.
An empty match pattern will return all files in the directory.

### Step 2

Run the program. There is only one command line parameter, which is the fully qualified path
to the ```Configuration.json``` file. If this parameter isn't specified, it defaults to
the current executing location.

Specifying a different location for the settings file:

    Perjure.exe "D:\Configuration.json"

Using the default settings file:

    Perjure.exe

> Note: The program does not prompt before deleting files. You should be sure that the
> directories specified in the settings file are what you *actually* want the program
> to purge before running the program.

## Exit Codes
The program will return different exit codes so that appropriate error handling can be
added to the calling application. This allows enterprise job schedulers, for example,
to fail the job if Perjure returns an exit code other than ```0```.

* **0 - Success:** The program completed normally
* **1 - Invalid Configuration:** The configuration file is not formatted correctly or the file could not be found
* **2 - Directory Not Found:** A directory in the configuration file was not found and could not be purged
* **4 - File Not Deleted:** A file was not deleted, which can be caused by any number of reasons. The file may be marked as read-only, the user account running Perjure does not have access to delete the file, or the file existed when the list of files to delete was constructed but was deleted by another user or process before Perjure could delete the file.

Since the exit codes are a C# Flags Enum, additional exit codes can be returned which
are a combination of those above. For example, ```6``` could be returned, which means
that both a directory was not found and a file was not deleted.
