using Dietetons.DataBase;
using Dietetons.Utils;
using NUnit.Framework;
using System;

namespace DietetonsTests
{
    [TestFixture]
    public class LocalDataBaseTests
    {
        [Test]
        public void AddClientTest()
        {
            var localDataBaseDataManager = new LocalDBDataManager();
            var john = new Client()
            {
                Name = "John",
                Surname = "Doe",
                EmailAddress = "john.doe@gmail.com",
                Phone = "+33647899026",
                BirthDate = new DateTime(1996, 02, 12),
                Height = 174.5,
                Weight = 70
            };
            var success = localDataBaseDataManager.AddClient(john);
            Assert.IsTrue(success);
        }
    }
}
