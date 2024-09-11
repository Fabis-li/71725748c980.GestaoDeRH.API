using GestaoDeRH.Dominio.Ferias;

namespace GestaoDeRH.Dominio.Interfaces;

public interface IRepositorioSolicitarFerias
{
    Task<SolicitarFerias> ObterPorId(int id);
    Task<List<SolicitarFerias>> ListarFeriasPorPeriodo(DateTime dataInicio, DateTime dataFim);
    Task<List<SolicitarFerias>> ListarFeriasPorColaborador(int colaboradorId);
}