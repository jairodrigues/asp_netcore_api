using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkshopAspCore.Model;
using Microsoft.EntityFrameworkCore;

namespace WorkshopAspCore.Controllers
{
    [Route("api/pessoas")]
    public class PessoasController : Controller
    {
        private readonly DataContext _dataContext;

        public PessoasController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            var pessoas = await _dataContext.Pessoas.ToListAsync();
            return Ok(pessoas);
        }

        [HttpPost]
        public async Task<IActionResult> PostPessoas([FromBody]Pessoa pessoa)
        {
            if (pessoa == null)
            {
                return BadRequest();
            }

            await _dataContext.Pessoas.AddAsync(pessoa);
            await _dataContext.SaveChangesAsync();

            //return Ok(pessoa);
            return this.CreatedAtRoute("GetPessoa", new { id = pessoa.Id }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoas(int id, [FromBody] Pessoa pessoa)
        {

            var todo = _dataContext.Pessoas.FirstOrDefault(t => t.Id == id);

            todo.Nome = pessoa.Nome;
            todo.Twitter = pessoa.Twitter;

            _dataContext.Pessoas.Update(todo);
            await _dataContext.SaveChangesAsync();

            return new ContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoas(int id)
        {
            var aux = await _dataContext.Pessoas.FirstOrDefaultAsync(t => t.Id == id);

            if (aux == null)
            {
                return NotFound();
            }

            _dataContext.Pessoas.Remove(aux);
            await _dataContext.SaveChangesAsync();

            return Ok(aux);
        }

        [HttpGet("{id}", Name = "GetPessoa")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _dataContext.Pessoas.FirstOrDefaultAsync(t => t.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);

        }

    }
}