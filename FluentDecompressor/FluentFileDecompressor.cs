using System;
using System.IO;
using SharpCompress;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Rar;

namespace FluentDecompressor
{
    public class FluentFileDecompressor
    {
        public string ArchivePath { get; set; }
        public string ArchivePassword { get; private set; }
        public string OutputDirectory { get; private set; }

        public FluentFileDecompressor(string archivePath)
        {
            ArchivePath = archivePath.Trim();
        }

        public static FluentFileDecompressor ForArchive(string archivePath)
        {
            return new FluentFileDecompressor(archivePath.Trim());
        }

        public FluentFileDecompressor WithPassword(string password)
        {
            ArchivePassword = password;
            return this;
        }

        public FluentFileDecompressor ToOutputDirectory(string outputDirectory)
        {
            OutputDirectory = outputDirectory;
            return this;
        }

        public FluentFileDecompressor DecompressInto(string outputDirectory)
        {
            return ToOutputDirectory(outputDirectory).Decompress();
        }

        public FluentFileDecompressor Decompress()
        {
            if (string.IsNullOrWhiteSpace(OutputDirectory) || string.IsNullOrWhiteSpace(ArchivePath))
                throw new InvalidOperationException("Output and Archive paths should be defined.");

            if (ArchivePath.EndsWith(".rar"))
            {
                DecompressRar();
            }

            return this;
        }

        private void DecompressRar()
        {
            var archiveOptions = new ReaderOptions();
            if(!string.IsNullOrWhiteSpace(ArchivePassword))
            {
                archiveOptions.Password = ArchivePassword;
            }

            using (var reader = RarReader.Open(File.OpenRead(ArchivePath), archiveOptions))
            {
                while (reader.MoveToNextEntry())
                {
                    reader.WriteEntryToDirectory(OutputDirectory,
                                                 new ExtractionOptions()
                                                 {
                                                     ExtractFullPath = true,
                                                     Overwrite = true
                                                 });
                }
            }
            //RarArchive archive = RarArchive.Open(ArchivePath);
            //foreach (RarArchiveEntry entry in archive.Entries)
            //{
            //    try
            //    {
            //        string fileName = Path.GetFileName(entry.FilePath);
            //        string rootToFile = Path.GetFullPath(entry.FilePath).Replace(fileName, "");

            //        if (!Directory.Exists(rootToFile))
            //        {
            //            Directory.CreateDirectory(rootToFile);
            //        }

            //        entry.WriteToFile(rootToFile + fileName, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
            //    }
            //    catch (Exception ex)
            //    {
            //        //handle your exception here..
            //    }
            //}
        }
    }
}
