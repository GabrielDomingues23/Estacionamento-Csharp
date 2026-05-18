namespace EstacionamentoAPI.Models;

public class Vaga {
    public int Id { get; set; }
    public string NumeroVaga { get; set; } = string.Empty;
    public StatusEstadia Status { get; set; }
    public DateTime? HoraEntrada { get; set; }

    public int? CarroId { get; set; }
    public Carro? Carro { get; set; }
}