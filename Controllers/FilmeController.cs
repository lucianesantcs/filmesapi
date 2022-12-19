using System;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context) 
        {
          _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null) {
              return Ok(filme);
            } else {
              return NotFound();
            }

        }

        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] filmeNovo) 
        {
          Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id === id);
          if (filme == null) {
            return NotFound();
          }
          filme.Titulo = filmeNovo.Titulo;
          filme.Diretor = filmeNovo.Diretor;
          filme.Genero = filmeNovo.Genero;
          filme.Duracao = filmeNovo.Duracao;
          _context.SaveChanges();
          return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
          Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id === id);
          if (filme == null) {
            return NotFound();
          }
          _context.Remove(filme);
          _context.SaveChanges();
          return NoContent();
        }
    }
}