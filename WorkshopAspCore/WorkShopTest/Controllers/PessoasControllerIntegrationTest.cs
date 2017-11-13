using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkshopAspCore.Model;
using WorkshopAspCore.TestUnit.TestUnit.Configuration;
using Xunit;

namespace WorkshopAspCore.TestUnit.TestUnit.Controllers
{
    public class PessoasControllerIntegrationTest : BaseIntegrationTest
    {
        private const string BaseURL = "/api/pessoas/";

        public PessoasControllerIntegrationTest(BaseTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task DeveRetornarListaVazia()
        {
            var response = await Client.GetAsync(BaseURL);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);

            Assert.Equal(data.Count, 0);

        }

        [Fact]
        public async Task DeveRetornarLista()
        {
            var pessoa = new Pessoa
            {
                Nome = "Jairo",
                Twitter = "@jairodrigues"
            };

            await TesteDataContext.AddAsync(pessoa);
            await TesteDataContext.SaveChangesAsync();

            var response = await Client.GetAsync(BaseURL);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);

            Assert.Equal(data.Count, 1);
            Assert.Contains(data, x => x.Nome == pessoa.Nome);
        }




    }
}
