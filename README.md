# FluentDecompressor

A small library to use fluent syntax to decompress archives.

Currently, this library supports only version 4 RAR file format. This library is powered by [SharpCompress](https://github.com/adamhathcock/sharpcompress).

## Usage

For example,

```C#
using FluentDecompressor;

//...
FluentFileDecompressor
    .ForArchive(archivePath)
    .WithPassword(password)   // Password is optional
    .DecompressInto(outputDirectory);
```

## Installation

Install the Nuget, which can be installed using this command:

```
Install-Package FluentDecompressor
```

## License

MIT License.

*SharpCompress* has [its own License](https://github.com/adamhathcock/sharpcompress/blob/master/LICENSE.txt).