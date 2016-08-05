using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using AlwaysMoveForward.OAuth.Client;

namespace AlwaysMoveForward.OAuth.UnitTests.CommonTests
{
    [TestFixture]
    public class RealmTests
    {
        private const string TestRealmStringBase = "Digital/Social/1/artie@test.com";
        private const string TestRealmString = "urn://Digital/Social/1/artie@test.com";

        [Test]
        public void RealmTestParse()
        {
            Realm parsedRealm = Realm.Parse(TestRealmString);

            Assert.IsNotNull(parsedRealm);
            Assert.IsTrue(parsedRealm.Area == "Digital");
            Assert.IsTrue(parsedRealm.Service == "Social");
            Assert.IsTrue(parsedRealm.DataId == "1");
            Assert.IsTrue(parsedRealm.DataName == "artie@test.com");
        }

        [Test]
        public void RealmTestToString()
        {
            Realm testRealm = new Realm() { Area = "Digital", Service = "Social", DataId = "1", DataName = "artie@test.com" };
            string realmString = testRealm.ToString();

            Assert.IsTrue(realmString == TestRealmString);
        }

        [Test]
        public void RealmTestParseBadUrn()
        {
            Realm parsedRealm = Realm.Parse("fail" + TestRealmString);

            Assert.IsNotNull(parsedRealm);
            Assert.IsTrue(parsedRealm.Area == "Digital");
            Assert.IsTrue(parsedRealm.Service == "Social");
            Assert.IsTrue(parsedRealm.DataId == "1");
            Assert.IsTrue(parsedRealm.DataName == "artie@test.com");
        }

        [Test]
        public void RealmTestParseNoUrn()
        {
            Realm parsedRealm = Realm.Parse(TestRealmStringBase);

            Assert.IsNotNull(parsedRealm);
            Assert.IsTrue(parsedRealm.Area == "Digital");
            Assert.IsTrue(parsedRealm.Service == "Social");
            Assert.IsTrue(parsedRealm.DataId == "1");
            Assert.IsTrue(parsedRealm.DataName == "artie@test.com");
        }
    }
}
