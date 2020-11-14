using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteContasPagar.Dal;
using TesteContasPagar.Models;


namespace TesteContasPagar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasPagarController : ControllerBase
    {
        private readonly ContasPagarContext _context;

        public ContasPagarController(ContasPagarContext context)
        {
            _context = context;
        }

        // GET: api/ContasPagar
        [HttpGet]
        public List<ContaPagar> GetContaPagar()
        {
            return  _context.ContaPagar.Include(p => p.RegraAtraso).ToList();
        }

        // GET: api/ContasPagar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContaPagar>> GetContaPagar(int id)
        {
            var contaPagar = await _context.ContaPagar.FindAsync(id);

            if (contaPagar == null)
            {
                return NotFound();
            }

            return contaPagar;
        }

        // PUT: api/ContasPagar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContaPagar(int id, ContaPagar contaPagar)
        {
            if (id != contaPagar.Id)
            {
                return BadRequest();
            }

            _context.Entry(contaPagar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaPagarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContasPagar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ContaPagar>> PostContaPagar(ContaPagar contaPagar)
        {

            if (ValidaCampos(contaPagar))
            {
                TimeSpan date = Convert.ToDateTime(contaPagar.DataPagamento) - Convert.ToDateTime(contaPagar.DataVencimento);
                int diasAtraso = date.Days;

                if(diasAtraso > 0)
                {
                    contaPagar.DiasAtraso = diasAtraso;
                    var regraAtraso = _context.RegraAtraso.OrderByDescending(x => x.DiasAtraso).FirstOrDefault(x => x.DiasAtraso <= diasAtraso);
                    if(regraAtraso != null)
                    {
                        contaPagar.RegraAtrasoId = regraAtraso.Id;
                        contaPagar.ValorCorrigido = contaPagar.ValorOriginal + (contaPagar.ValorOriginal / 100 * regraAtraso.Multa) + (contaPagar.ValorOriginal / 100 * (regraAtraso.JurosDia * diasAtraso));
                    }
                }
                else
                    contaPagar.DiasAtraso = 0;
                _context.ContaPagar.Add(contaPagar);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetContaPagar", new { id = contaPagar.Id }, contaPagar);
            }
            else
                return BadRequest();

        }

        // DELETE: api/ContasPagar/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContaPagar>> DeleteContaPagar(int id)
        {
            var contaPagar = await _context.ContaPagar.FindAsync(id);
            if (contaPagar == null)
            {
                return NotFound();
            }

            _context.ContaPagar.Remove(contaPagar);
            await _context.SaveChangesAsync();

            return contaPagar;
        }

        private bool ContaPagarExists(int id)
        {
            return _context.ContaPagar.Any(e => e.Id == id);
        }

        private bool ValidaCampos(ContaPagar contaPagar)
        {
            if (contaPagar.DataPagamento == null)
                return false;
            else if (contaPagar.DataVencimento == null)
                return false;
            else if (string.IsNullOrEmpty(contaPagar.Nome))
                return false;
            else if (contaPagar.ValorOriginal <= 0)
                return false;
            else
                return true;
        }
    }
}
