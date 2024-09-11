using GestaoDeRH.Dominio.Base;
using GestaoDeRH.Dominio.Pessoas;

namespace GestaoDeRH.Dominio.Ferias
{
    public class SolicitarFerias : Entidade
    {
        public int ColaboradorId { get; set; }
        public virtual Colaborador? Colaborador { get; set; }

        public DateTime DataInicioFerias { get; set; }
        public DateTime DataFimFerias { get; set; }
        public DateTime Solicitacao { get; set; }

        public SolicitarFerias() { }
        
        public SolicitarFerias(DateTime solicitacao)
        {
            Solicitacao = DateTime.Now;
        }

        public List<string> Validar()
        {
            var erros = new List<string>();

            if(ColaboradorId == 0)
            {
                erros.Add("Colaborador é obrigatório.");
            }

            if (DataInicioFerias == DateTime.MinValue)
            {
                erros.Add("Data de início das férias é obrigatória.");
            }

            if (DataFimFerias == DateTime.MinValue)
            {
                erros.Add("Data de fim das férias é obrigatória.");
            }

            if((DataFimFerias- DataInicioFerias).TotalDays > 30)
            {
                erros.Add("As férias não podem durar mais de 30 dias.");
            }

            if (DataInicioFerias != DateTime.MinValue && DataFimFerias != DateTime.MinValue)
            {
                if (DataInicioFerias > DataFimFerias)
                {
                    erros.Add("Data de início das férias deve ser anterior à data de fim das férias.");
                }
            }

            return erros;
        }

    }
}