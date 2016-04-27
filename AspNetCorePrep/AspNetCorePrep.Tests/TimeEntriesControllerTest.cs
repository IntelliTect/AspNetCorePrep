using AspNetCorePrep.Controllers;
using System.Linq;
using Xunit;

namespace AspNetCorePrep.Tests
{
    public class TimeEntriesControllerTest : IClassFixture<DatabaseFixtureInMemory>
    {
        public TimeEntriesControllerTest(DatabaseFixtureInMemory dbFixture)
        {
            DbFixture = dbFixture;
        }
        private DatabaseFixtureInMemory DbFixture { get; set; }

        [Fact]
        public void HoursTest()
        {
            var tec = new TimeEntriesController(DbFixture.Db);
            Assert.NotNull(tec);

            var result = tec.TotalHours();
            Assert.Equal(12, result);

            result = tec.TotalHours("Test");
            Assert.Equal(0, result);
        }
    }
}
