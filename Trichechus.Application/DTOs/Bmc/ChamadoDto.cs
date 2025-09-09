namespace Trichechus.Application.DTOs.Bmc;

public class ChamadoRequestDto
{
	public string Numero { get; set; } = default!;
	public string DWP { get; set; } = default!;
	public string chamadoDescricao { get; set; } = default!;
	public string Status { get; set; } = default!;
}

public class ChamadoResponseDto
{
	public string Numero { get; set; } = default!;
	public string DWP { get; set; } = default!;
	public string Solicitante { get; set; } = default!;
	public string Solicitante2 { get; set; } = default!;
	public string Prioridade { get; set; } = default!;
	public string Abertura { get; set; } = default!;
	public string Fechamento { get; set; } = default!;
	public string Analista { get; set; } = default!;
	public string Servico { get; set; } = default!;
	public string chamadoDescricao { get; set; } = default!;
	public string Status { get; set; } = default!;

	public string Link { get; set; } = default!;
	public string URL { get; set; } = default!;
}