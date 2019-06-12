using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using spacex.api.Repositories.Interfaces;
using spacex.api.Models;
using spacex.api.Services;

namespace spacex.api.tests
{
    [TestClass]
    public class LaunchpadServiceTests
    {
        [TestMethod]
        public void TestShouldReturnAll()
        {
            // arrange
            var repositoryData = new List<Launchpad>
            {
                new Launchpad
                {
                    Id = 1,
                    Name = "Launchpad 1",
                    Status = "active",
                },
                new Launchpad
                {
                    Id = 2,
                    Name = "Launchpad 2",
                    Status = "active",
                },
            };

            var repository = new Mock<ILaunchpadRepository>();
            repository.Setup(x => x.Get(It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult<IList<Launchpad>>(repositoryData));
            var service = new LaunchpadService(repository.Object);

            // act
            var result = service.Get(null, null).Result;

            // assert
            Assert.AreEqual(result, repositoryData);
            Assert.AreEqual(result.Count, repositoryData.Count);
        }

        [TestMethod]
        public void TestShouldReturnFirst()
        {
            // arrange
            var repositoryData = new List<Launchpad>
            {
                new Launchpad
                {
                    Id = 1,
                    Name = "Launchpad 1",
                    Status = "active",
                }
            };

            var repository = new Mock<ILaunchpadRepository>();
            repository.Setup(x => x.Get(It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult<IList<Launchpad>>(repositoryData));
            var service = new LaunchpadService(repository.Object);

            // act
            var result = service.Get(1, null).Result;

            // assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Id, 1);
        }
    }
}
