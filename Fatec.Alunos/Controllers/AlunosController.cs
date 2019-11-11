using Fatec.Alunos.Data;
using Fatec.Alunos.Models;
using Fatec.Alunos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Fatec.Alunos.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AlunosController()
        {
            //_hubContext = new ;
            _context = new ApplicationDbContext();

        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aluno.ToListAsync());
        }

        public string DataHoraAtual()
        {
            var alunoList = _context.Aluno.ToArray();

            if (alunoList.Any())
            {
                var rnd = new Random();
                int index = rnd.Next(alunoList.Count());

                var alunoSorteado = alunoList[index];

                return $"{alunoSorteado.Nome} - {alunoSorteado.Documento} - {alunoSorteado.NomeComputador}";
            }
            else
            {
                return "Nenhum aluno cadastrado.";

            }
        }

        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Documento,Ip,NomeComputador")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Id = Guid.NewGuid();
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Documento,Ip,NomeComputador")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                var alunos = _context.Aluno.ToArray();
                _context.Aluno.RemoveRange(alunos);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            var aluno = await _context.Aluno
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(Guid id)
        {
            return _context.Aluno.Any(e => e.Id == id);
        }

        [HttpPost]
        [AllowAnonymous]
        public RetornoWs Inserir([FromBody]Aluno aluno)
        {
            try
            {
                using var _context = new ApplicationDbContext();

                _context.Aluno.Add(aluno);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new RetornoWs { Codigo = 200, Mensagem = $"Ocorreu um erro ao cadastrar o Aluno. {ex.ToString()}" };

            }

            return new RetornoWs { Codigo = 100, Mensagem = "Aluno cadastrado com sucesso." };
        }
    }
}
