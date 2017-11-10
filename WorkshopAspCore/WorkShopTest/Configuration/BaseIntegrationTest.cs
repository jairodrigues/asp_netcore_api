using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkshopAspCore.Model;
using Xunit;

namespace WorkshopAspCore.TestUnit.TestUnit.Configuration
{
    [Collection("Base collection")]
    public abstract class BaseIntegrationTest
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly DataContext TesteDataContext;
        protected BaseTestFixture Fixture { get; }

        protected BaseIntegrationTest(BaseTestFixture fixture)
        {
            Fixture = fixture;

            TesteDataContext = fixture.TesteDataContext;
            Server = fixture.Server;
            Client = fixture.Client;

            ClearDb().Wait();
        }

        private async Task ClearDb()
        {
            var commands = new[]
            {
                "EXEC sp_MsForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'",
                "EXEC sp_MsForEachTable 'DELETE FROM ?'",
                "EXEC sp_MsForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'",
            };

            await TesteDataContext.Database.OpenConnectionAsync();

            foreach(var comand in commands)
            {
                await TesteDataContext.Database.ExecuteSqlCommandAsync(comand);
            }

            TesteDataContext.Database.CloseConnection();
        }
    }
}
