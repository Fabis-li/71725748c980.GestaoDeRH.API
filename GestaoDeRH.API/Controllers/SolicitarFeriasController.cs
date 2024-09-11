using GestaoDeRH.Aplicacao.Ferias.DTO;
using GestaoDeRH.Aplicacao.Ferias.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeRH.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SolicitarFeriasController : Controller
{
    private readonly ISolicitarFerias _solicitarFerias;

    public SolicitarFeriasController(ISolicitarFerias solicitarFerias)
    {
        _solicitarFerias = solicitarFerias;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<SolicitarFeriasDto>>> ListarSolitacoesFerias()
    {
        var solicitacao = await _solicitarFerias.Listar();

        return Ok(solicitacao);
    }

    [HttpPost("")]
    public async Task<IActionResult> CriarSolicitacaoFerias(SolicitarFeriasDto novaSolicitacaoFeriasDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var resultado = await _solicitarFerias.SolicitarFerias(novaSolicitacaoFeriasDto);

        if (!resultado.Sucesso)
        {
            return BadRequest(new { msg = string.Join("; ", resultado.Erros) });
        }

        return CreatedAtAction(nameof(ListarSolitacoesFerias),
            new { id = resultado.Dados.ColaboradorId }, resultado.Dados);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarSolicitacaoFerias(int id, SolicitarFeriasDto solicitarFeriasDto)
    {
        if (id <= 0)
            return BadRequest("Id inválido.");

        var resultado = await _solicitarFerias.Editar(solicitarFeriasDto);

        if (!resultado.Sucesso)
            return BadRequest(new { msg = string.Join("; ", resultado.Erros) });


        if (resultado.Dados is null)
            return NotFound("Solivitação de férias não encontrada.");

        return Ok(resultado.Dados);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarSolicitacaoFerias(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido.");

        var resultado = await _solicitarFerias.Excluir(id);

        if (!resultado.Sucesso)
            return BadRequest(new { msg = string.Join("; ", resultado.Erros) });

        return Ok(new { msg = "Solicitaçao de férias excluida com sucesso." });
    }
}