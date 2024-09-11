using GestaoDeRH.Aplicacao.Base;
using GestaoDeRH.Aplicacao.Ferias.DTO;
using GestaoDeRH.Aplicacao.Ferias.Interfaces;
using GestaoDeRH.Dominio.Ferias;
using GestaoDeRH.Dominio.Interfaces;
using GestaoDeRH.Dominio.Pessoas;

namespace GestaoDeRH.Aplicacao.SolicitacaoFerias;

public class SolicitarFeriasService : ISolicitarFerias
{
    private readonly IRepositorio<SolicitarFerias> _repositorioSolicitarFerias;
    private readonly IRepositorio<Colaborador> _repositorioColaborador;

    public SolicitarFeriasService(
        IRepositorio<SolicitarFerias> repositorioSolicitarFerias,
        IRepositorio<Colaborador> repositorioColaborador
    )
    {
        _repositorioSolicitarFerias = repositorioSolicitarFerias;
        _repositorioColaborador = repositorioColaborador;
    }

    public async Task<ResultadoOperacao<SolicitarFeriasDto>> SolicitarFerias(SolicitarFeriasDto dto)
    {
           var resultado = new ResultadoOperacao<SolicitarFeriasDto>();

        var colaborador = await _repositorioColaborador.Obter(dto.ColaboradorId);

        if (colaborador is null)
        {
            resultado.AdicionarErros(new List<string>{"Colaborador não encontrado"});
            return resultado;
        }

        var contrato = colaborador.DataInicioContratoDeTrabalho.AddYears(1);

        if (dto.DataInicio < contrato && DateTime.Now < contrato)
        {
            resultado.AdicionarErros(new List<string> {$"Colocaborador ainda não tem um ano de contrato"});
            return resultado;
        }

        var solicitacao = new SolicitarFerias
        {
            ColaboradorId = dto.ColaboradorId,
            DataInicioFerias = dto.DataInicio,
            DataFimFerias = dto.DataFim,
            Solicitacao = DateTime.Now
        };

        if (!solicitacao.EstaValida(out var erros))
        {
            resultado.AdicionarErros(erros);
            return resultado;
        }

        if (await VerificarSolicitaçãoExistente(dto.ColaboradorId, dto.DataInicio, dto.DataFim))
        {
            resultado.AdicionarErros((new List<string> {"Existe uma solicitação neste periodo para este colaborador"}));
            return resultado;
        }

        var diasSolicitadosAno = await ObterDiasSolicitadosAno(dto.ColaboradorId, dto.DataInicio.Year);

        var diasSolicitados = dto.CalcularDiasFerias();

        if (diasSolicitadosAno + diasSolicitados > 30)
        {
            resultado.AdicionarErros(new List<string> {$"Total de dias solicitados: {diasSolicitadosAno + diasSolicitados} dias, total de dias disponíveis: 30 dias."});

            return resultado;
        }

        await _repositorioSolicitarFerias.Salvar(solicitacao);

        resultado.Dados = new SolicitarFeriasDto
        {
            ColaboradorId = solicitacao.ColaboradorId,
            DataInicio = solicitacao.DataInicioFerias,
            DataFim = solicitacao.DataFimFerias
        };

        return resultado;

    }

    private async Task<int> ExcedeLimiteDeDias(SolicitarFeriasDto dto)
    {
        var diasSolicitadosAno = await ObterDiasSolicitadosAno(dto.ColaboradorId, dto.DataInicio.Year);

        var diasSolicitados = dto.CalcularDiasFerias();
        
        return diasSolicitadosAno + diasSolicitados;
    }


    public async Task<SolicitarFeriasDto> ObterPorId(int id)
    {
        var solicitacao = await _repositorioSolicitarFerias.Obter(id);
        
        if (solicitacao is null)
            return null;

        return new SolicitarFeriasDto
        {
            ColaboradorId = solicitacao.ColaboradorId,
            DataInicio = solicitacao.DataInicioFerias,
            DataFim = solicitacao.DataFimFerias
        };
    }

    public async Task<ResultadoOperacao<SolicitarFeriasDto>> Editar(SolicitarFeriasDto dto)
    {
        var resultado = new ResultadoOperacao<SolicitarFeriasDto>();

        var solicitacaoSelecionada = await _repositorioSolicitarFerias.Obter(dto.ColaboradorId);

        if (solicitacaoSelecionada is null)
        {
            resultado.AdicionarErros(new List<string> { "Solicitação não encontrada" });

            return resultado;
        }

        solicitacaoSelecionada.ColaboradorId = dto.ColaboradorId;
        solicitacaoSelecionada.DataInicioFerias = dto.DataInicio;
        solicitacaoSelecionada.DataFimFerias = dto.DataFim;

        if (!solicitacaoSelecionada.EstaValida(out var erros))
        {
            resultado.AdicionarErros(erros);
            return resultado;
        }

        await _repositorioSolicitarFerias.Salvar(solicitacaoSelecionada);

        resultado.Dados = dto;
        
        return resultado;
    }

    public async Task<ResultadoOperacao<SolicitarFeriasDto>> Excluir(int id)
    {
        var resultado = new ResultadoOperacao<SolicitarFeriasDto>();

        var solicitacao = await _repositorioSolicitarFerias.Obter(id);

        if (solicitacao is null)
        {
            resultado.AdicionarErros(new List<string> {"Solicitação não encontrada"});
            
            return resultado;
        }

        await _repositorioSolicitarFerias.Deletar(id);

        return resultado;
    }

    public async Task<List<SolicitarFeriasDto>> Listar()
    {
        var solicitacao = await _repositorioSolicitarFerias.Listar();

        return solicitacao.Select(s => new SolicitarFeriasDto
        {
            Id = s.Id,
            ColaboradorId = s.ColaboradorId,
            DataInicio = s.DataInicioFerias,
            DataFim = s.DataFimFerias
        }).ToList();
    }
    private async Task<int> ObterDiasSolicitadosAno(int colaboradorId, int ano)
    {
        var solicitacao = await _repositorioSolicitarFerias.Listar();

        return solicitacao
            .Where(s => s.ColaboradorId == colaboradorId && s.DataInicioFerias.Year == ano)
            .Sum(s => (s.DataFimFerias - s.DataInicioFerias).Days + 1);
    }
    private async Task<bool> VerificarSolicitaçãoExistente(int colaboradorId, DateTime DataInicio, DateTime DataFim)
    {
        var solicitacao = await _repositorioSolicitarFerias.Listar();

        return solicitacao.Any(s => s.ColaboradorId == colaboradorId &&
                                    (s.DataInicioFerias <= DataFim && s.DataInicioFerias >= DataInicio ||
                                     s.DataFimFerias <= DataFim && s.DataFimFerias >= DataInicio ||
                                     s.DataInicioFerias <= DataInicio && s.DataFimFerias >= DataFim));
    }
   
}
