using GestaoDeRH.Aplicacao.Base;
using GestaoDeRH.Aplicacao.Ferias.DTO;

namespace GestaoDeRH.Aplicacao.Ferias.Interfaces;

public interface ISolicitarFerias
{
    Task<ResultadoOperacao<SolicitarFeriasDto>> SolicitarFerias(SolicitarFeriasDto dto);
    Task<SolicitarFeriasDto> ObterPorId(int id);
    Task<ResultadoOperacao<SolicitarFeriasDto>> Editar(SolicitarFeriasDto dto);
    Task<ResultadoOperacao<SolicitarFeriasDto>> Excluir(int id);
    Task<List<SolicitarFeriasDto>> Listar();
}