using System;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentDecompressor.Tests
{
    [TestClass]
    public class DecompressionTests
    {
        [TestMethod]
        public void TestDecompression()
        {
            // ARRANGE
            var subdirectoryName = Guid.NewGuid().ToString();
            var outputDirectory = Path.Combine(Environment.CurrentDirectory, subdirectoryName);
            var archivePath = Path.Combine(Environment.CurrentDirectory, "ExampleRAR.rar");
            var password = "example";

            // ACT
            Directory.CreateDirectory(outputDirectory);

            FluentFileDecompressor
                .ForArchive(archivePath)
                .WithPassword(password)
                .DecompressInto(outputDirectory);

            // ASSERT
            Directory
                .GetFileSystemEntries(outputDirectory)
                .Should().NotHaveCount(0);
        }
    }
}
