using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.ViewModels;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.UnitTests.Fakes;

namespace ZbW.Testing.Dms.Client.UnitTests
{
    [TestFixture]
    public class FileHandlerTest
    {
        GuidHandler g = new GuidHandler();
        String repositoryPath = "C:\\Temp\\DMS";
        //UnitOfWorkName_Szenario_ErwartetesVerhalten
        //Division_divideByzero_ThrowsException
        [Test]
        public void GetDestinationPath_Returns_DestinationPath()
        {
            // arrange
            FileHandler sut = new FileHandler(repositoryPath, g);
            DateTime date = new DateTime(2019, 02, 20);

            // act
            string destinationPath = sut.GetDestinationPath(date);

            // assert
            Assert.That(destinationPath, Is.EqualTo("C:\\Temp\\DMS\\2019"));
        }

        [Test]
        public void GetFileDestinationPath_Returns_FileDestinationPath()
        {
            //arrange
            FileHandler sut = new FileHandler(repositoryPath, g);
            String destinationPath = "C:\\Temp\\DMS\\2018";
            String guid = "1234";
            String fileName = "abc.txt";

            //act
            string fileDestinationPath = sut.GetFileDestinationPath(destinationPath, guid, fileName);

            //assert
            Assert.That(fileDestinationPath, Is.EqualTo("C:\\Temp\\DMS\\2018\\1234_Content.txt"));
        }

        [Test]
        public void GetDocuments_WithTypeAndTerm_ReturnsResults()
        {
            //arrange
            FileHandler sut = new FileHandler(repositoryPath, g);
            String selectedTypItem = "Verträge";
            String suchbegriff = "abc";
            List<MetadataItem> list;

            //act
            list = sut.GetDocuments(selectedTypItem, suchbegriff);

            //assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetDocuments_WithTerm_ReturnsResults()
        {
            //arrange
            FileHandler sut = new FileHandler(repositoryPath, g);
            String selectedTypItem = null;
            String suchbegriff = "abc";
            List<MetadataItem> list;

            //act
            list = sut.GetDocuments(selectedTypItem, suchbegriff);

            //assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetDocuments_WithType_ReturnsResults()
        {
            //arrange
            FileHandler sut = new FileHandler(repositoryPath, g);
            String selectedTypItem = "Verträge";
            String suchbegriff = "";
            List<MetadataItem> list;

            //act
            list = sut.GetDocuments(selectedTypItem, suchbegriff);

            //assert
            Assert.That(list.Count, Is.GreaterThan(0));
        }

        [Test]
        public void GetDocuments_WithoutTypeAndTerm_ThrowsException()
        {
            //arrange
            FileHandler sut = new FileHandler(repositoryPath, g);
            String selectedTypItem = null;
            String suchbegriff = "";

            //act
            var ex = Assert.Throws<ArgumentException>(() => sut.GetDocuments(selectedTypItem, suchbegriff));

            //assert
            Assert.That(ex.Message, Is.EqualTo("Type AND searchterm can't be empty"));
        }

        [Test]
        public void CreateGUID_WithStub_Returns1234()
        {
            //arrange
            GUIDStub fake = new GUIDStub();
            FileHandler sut = new FileHandler("C:\\Tempt\\DMS", fake);

            //act
            String result = sut.CreateGUID();

            //assert
            Assert.That(result, Is.EqualTo("1234"));           
        }


        [Test]
        public void CreateGUID_WithMock_CalledCorrect()
        {
            //arrange
            GUIDMock fake = new GUIDMock();
            FileHandler sut = new FileHandler("C:\\Tempt\\DMS", fake);

            //act
            sut.CreateGUID();

            //assert
            Assert.That(fake.GuidCalled, Is.True);
        }
    }
}
