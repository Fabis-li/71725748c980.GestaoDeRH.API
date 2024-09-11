using GestaoDeRH.Dominio.Pessoas;

namespace GestaoDeRh.Tests.Ferias
{
    public class SolicitarFeriasTests
    {
        [Theory]
        [InlineData(1, "2021-01-01", "2021-01-30", "2021-01-01", 0)]
        [InlineData(1, "2021-01-01", "2021-01-30", "2021-01-01", 1)]
        [InlineData(1, "2021-01-01", "2021-01-30", "2021-01-01", 2)]
        public void SolicitarFeriasTestes(int colaboradorId, string dataInicioFerias, string dataFimFerias, string solicitacaoDeFerias, int quantidadeErrosEsperada)
        {
            //arrange
            var colaborador = new Colaborador
            {
                Id = colaboradorId,
                DataInicioContratoDeTrabalho = new DateTime(2020, 01, 01),
                DataFimContratoDeTrabalho = null
            };
            //act
            

            //assert
            //var ferias = new Ferias(colaboradorId, DateTime.Parse(dataInicioFerias), DateTime.Parse(dataFimFerias), DateTime.Parse(solicitacaoDeFerias));
            //var erros = ferias.Validar();
            //Assert.Equal(quantidadeErrosEsperada, erros.Count);
        }
    }
}
