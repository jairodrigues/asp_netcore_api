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
            return Json(pessoas);
        }

        [HttpPost]
        public async Task<IActionResult> PostPessoas([FromBody]Pessoa pessoa)
        {
            await _dataContext.Pessoas.AddAsync(pessoa);
            await _dataContext.SaveChangesAsync();

            return Json(pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoas(int id,[FromBody]Pessoa pessoa)
        {

            var todo = _dataContext.Pessoas.FirstOrDefault(t => t.Id == id);
            _dataContext.Pessoas.Update(todo);
            _dataContext.SaveChanges();

            return Json(pessoa);
        }
    }
}