using Dietetons.DataBase;
using Dietetons.Utils;
using NUnit.Framework;
using System.Linq;

namespace DietetonsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAddClient()
        {
            var ravenDataProvider = new RavenDataProvider();
            var jhonDoe = new Client()
            {
                Name = "Jhon",
                Surname = "Doe",
                MailAddress = "jhon.doe@gmail.com",
                PhoneNumber = "+33678905346",
                Height = 184,
                Weight = 80,
                BirthDate = new System.DateTime(1996, 02, 12)
            };
            var success = ravenDataProvider.AddClient(jhonDoe);
            Assert.IsTrue(success);
            ravenDataProvider.GetMetaDataFor(jhonDoe.Id);
            var allClients = ravenDataProvider.GetAllClients();
            var jhonDoes = allClients.Where(x => x.Id == jhonDoe.Id);
            Assert.IsTrue(jhonDoes.Count() == 1);
        }
    }
}