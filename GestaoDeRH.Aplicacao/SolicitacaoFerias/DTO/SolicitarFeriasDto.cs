namespace GestaoDeRH.Aplicacao.Ferias.DTO;

public class SolicitarFeriasDto
{
    public int Id { get; set; }
    public int ColaboradorId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    
    public DateTime SolicitacaoDeFerias { get; set; }

    public bool ValidarDuracaoFerias()
    {
        return DataFim.Subtract(DataInicio).Days <= 30;
    }

    public int CalcularDiasFerias()
    {
        return DataFim.Subtract(DataInicio).Days + 1;
    }
}