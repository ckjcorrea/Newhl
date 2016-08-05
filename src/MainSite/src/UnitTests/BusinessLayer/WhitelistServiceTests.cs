using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.OAuth.Common.Configuration;
using AlwaysMoveForward.OAuth.UnitTests.Constants;

namespace AlwaysMoveForward.OAuth.UnitTests.BusinessLayer
{
    [TestFixture]
    public class WhitelistServiceTests : UnitTestBase
    {
        private static Uri testAllowAccessQueryString = new Uri("http://localhost:5000/Test/Folder/FileType.foo");
        private static Uri testDenyAccessQueryString = new Uri("http://localhost:5000/TestDeny/FolderDeny/FileType.fooDeny");

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessTest()
        {
            WhiteListConfiguration configuration = WhiteListConfiguration.GetInstance();

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccess(testAllowAccessQueryString, configuration);

            Assert.IsTrue(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessTest_Fail()
        {
            WhiteListConfiguration configuration = WhiteListConfiguration.GetInstance();

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccess(testDenyAccessQueryString, configuration);

            Assert.IsFalse(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessToFoldersTest()
        {
            string[] folders = new string[] { "Folder"};

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccessToFolders(testAllowAccessQueryString, folders);

            Assert.IsTrue(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessToFoldersTest_Fail()
        {
            string[] folders = new string[] { "Folder"};

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccessToFolders(testDenyAccessQueryString, folders);

            Assert.IsFalse(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessToFolderTest()
        {
            string folder = "Folder";

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccessToFolder(testAllowAccessQueryString, folder);

            Assert.IsTrue(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessToFolderTest_Fail()
        {
            string folder = "Folder";

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccessToFolder(testDenyAccessQueryString, folder);

            Assert.IsFalse(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessToFileTypesTest()
        {
            string[] fileTypes = new string[] { "foo"};

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccessToFileTypes(testAllowAccessQueryString, fileTypes);

            Assert.IsTrue(retVal);
        }

        [Test]
        public void WhitelistServiceAllowUnAuthorizedAccessToFileTypesTest_Fail()
        {
            string[] fileTypes = new string[] { "foo"};

            bool retVal = this.ServiceManager.WhiteListService.AllowUnauthorizedAccessToFileTypes(testDenyAccessQueryString, fileTypes);

            Assert.IsFalse(retVal);
        }
    }
}
