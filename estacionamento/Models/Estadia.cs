namespace EstacionamentoAPI.Models;

public class Estadia
{
    public int Id { get; set; }

    public int CarroId { get; set; }
    public Carro? Carro { get; set; }

    public int VagaId { get; set; }
    public Vaga? Vaga { get; set; }

    public DateTime DataEntrada { get; set; }
    public DateTime? DataSaida { get; set; }

    public decimal ValorCobrado { get; set; }

    public StatusEstadia Status { get; set; }
}