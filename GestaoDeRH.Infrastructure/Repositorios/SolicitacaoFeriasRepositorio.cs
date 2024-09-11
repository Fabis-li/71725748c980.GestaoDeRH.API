using GestaoDeRH.Dominio.Ferias;
using GestaoDeRH.Dominio.Interfaces;
using GestaoDeRH.Infra.BancoDeDados;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeRH.Infra.Repositorios;

public class SolicitacaoFeriasRepositorio(GestaoDeRhDbContext dbContext) : RepositorioGenerico<SolicitarFerias>(dbContext),  IRepositorioSolicitarFerias
{
    public Task<SolicitarFerias?> ObterPorId(int id)
    {
        return dbContext.SolicitarFerias.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<SolicitarFerias>> ListarFeriasPorPeriodo(DateTime dataInicio, DateTime dataFim)
    {
        throw new NotImplementedException();
    }

    public Task<List<SolicitarFerias>> ListarFeriasPorColaborador(int colaboradorId)
    {
        throw new NotImplementedException();
    }
}