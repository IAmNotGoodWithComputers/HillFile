using System;
using System.IO;
using System.Linq;
using HillFile.Lib.FileSystem;
using NUnit.Framework;

namespace HillFile.Tests.Integration.Lib.FileSystem
{
    [TestFixture]
    public class FileListingServiceTest
    {
        private string tmpFolder;
        
        [SetUp]
        public void Setup()
        {
            tmpFolder = Path.GetTempPath() + Guid.NewGuid();
            Directory.CreateDirectory(tmpFolder);
        }

        [Test]
        public void FileListingService_ListDirectories_CorrectAmount()
        {
            var fileListingService = new FileListingService();
            for (var i = 0; i < 3; i++)
            {
                Directory.CreateDirectory(tmpFolder + Path.DirectorySeparatorChar + Guid.NewGuid());
            }

            var directories = fileListingService.ListDirectories(tmpFolder)
                .ToList(); // ToList makes it easier to debug, than enumerable
            
            Assert.That(directories.Count(), Is.EqualTo(3));
        }

        [Test]
        public void FileListingService_ListFiles_CorrectAmount()
        {
            var fileListingService = new FileListingService();
            for (var i = 0; i < 3; i++)
            {
                File.Create(tmpFolder + Path.DirectorySeparatorChar + Guid.NewGuid());
            }

            var files = fileListingService.ListFiles(tmpFolder)
                .ToList(); // ToList makes it easier to debug, than enumerable
            
            Assert.That(files.Count(), Is.EqualTo(3));
        }

        [Test]
        public void FileListingService_ListFiles_CorrectName()
        {
            var fileListingService = new FileListingService();
            var fileName = Guid.NewGuid();
            File.Create(tmpFolder + Path.DirectorySeparatorChar + fileName);

            var files = fileListingService.ListFiles(tmpFolder);
            
            Assert.That(files.First().Name, Is.EqualTo(fileName.ToString()));
        }
        
        [Test]
        public void FileListingService_ListDirectories_CorrectName()
        {
            var fileListingService = new FileListingService();
            var directoryName = Guid.NewGuid();
            Directory.CreateDirectory(tmpFolder + Path.DirectorySeparatorChar + directoryName);

            var files = fileListingService.ListDirectories(tmpFolder);
            
            Assert.That(files.First().Name, Is.EqualTo(directoryName.ToString()));
        }
        
        [Test]
        public void FileListingService_LoadFileInfo_CorrectPath()
        {
            var fileListingService = new FileListingService();
            var fileName = Guid.NewGuid();
            var filePath = tmpFolder + Path.DirectorySeparatorChar + fileName;
            var fs = File.Create(filePath);
            fs.Dispose();
            File.WriteAllBytes(filePath, new byte[1337]);

            var fileInfo = fileListingService.LoadFileInfo(filePath);
            
            Assert.That(fileInfo.FullPath, Is.EqualTo(filePath));
            Assert.That(fileInfo.Name, Is.EqualTo(fileName.ToString()));
            Assert.That(fileInfo.Path, Is.EqualTo(tmpFolder));
        }
        
        [Test]
        public void FileListingService_LoadFileInfo_CorrectSize()
        {
            var fileListingService = new FileListingService();
            var fileName = Guid.NewGuid();
            var filePath = tmpFolder + Path.DirectorySeparatorChar + fileName;
            var fs = File.Create(filePath);
            fs.Dispose();
            File.WriteAllBytes(filePath, new byte[1337]);

            var fileInfo = fileListingService.LoadFileInfo(filePath);
            
            Assert.That(fileInfo.ByteSize, Is.EqualTo(1337));
        }
        
        [Test]
        public void FileListingService_LoadFileInfo_CorrectDates()
        {
            var fileListingService = new FileListingService();
            var fileName = Guid.NewGuid();
            var filePath = tmpFolder + Path.DirectorySeparatorChar + fileName;
            var fs = File.Create(filePath);
            fs.Dispose();
            File.WriteAllBytes(filePath, new byte[1337]);

            var fileInfo = fileListingService.LoadFileInfo(filePath);
            
            Assert.That(fileInfo.CreateDate, Is.GreaterThanOrEqualTo(DateTime.Now.Subtract(TimeSpan.FromMinutes(1))));
            Assert.That(fileInfo.CreateDate, Is.LessThanOrEqualTo(DateTime.Now.Add(TimeSpan.FromMinutes(1))));
            
            Assert.That(fileInfo.AccessDate, Is.GreaterThanOrEqualTo(DateTime.Now.Subtract(TimeSpan.FromMinutes(1))));
            Assert.That(fileInfo.AccessDate, Is.LessThanOrEqualTo(DateTime.Now.Add(TimeSpan.FromMinutes(1))));
            
            Assert.That(fileInfo.ModifiedDate, Is.GreaterThanOrEqualTo(DateTime.Now.Subtract(TimeSpan.FromMinutes(1))));
            Assert.That(fileInfo.ModifiedDate, Is.LessThanOrEqualTo(DateTime.Now.Add(TimeSpan.FromMinutes(1))));
        }
    }
}