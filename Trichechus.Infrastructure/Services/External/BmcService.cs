using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Trichechus.Application.DTOs.Bmc;
using Trichechus.Application.Interfaces.External;

namespace Trichechus.Infrastructure.Services.External;

public class BmcService : IBmcService
{
	private readonly HttpClient _httpClient;
	private readonly IConfiguration _config;

	public BmcService(HttpClient httpClient, IConfiguration config)
	{
		_httpClient = httpClient;
		_config = config;
	}

	public async Task<string> LoginAsync()
	{
		var login = new LoginRequest
		{
			// Username = _config["BMC:Username"],
			// Password = _config["BMC:Password"]
			Username = "bmc_api_gempi",
			Password = "UKIAE9g4Vnum9cP"
		};

		var response = await _httpClient.PostAsJsonAsync(
			"https://centraldeservicos-is.onbmc.com/api/rx/authentication/loginrequest", login);

		response.EnsureSuccessStatusCode();

		var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
		return result?.Token ?? throw new Exception("Token não retornado");
	}

	public async Task<List<ChamadoResponseDto>> BuscarChamadosAsync(
			BmcTicketType tipo,
			object parametros
		)
	{
		string url;
        string jsonBody;

        if (tipo == BmcTicketType.Incident)
        {
            var req = parametros as ChamadoRequestDto 
                      ?? throw new ArgumentException("Parâmetros inválidos para Incident");

            url = "https://centraldeservicos-is.onbmc.com/api/arsys/v1/entry/HPD:IncidentInterface";
            jsonBody = JsonSerializer.Serialize(req);
        }
        else if (tipo == BmcTicketType.WorkOrder)
        {
            var req = parametros as ChamadoRequestDto 
                      ?? throw new ArgumentException("Parâmetros inválidos para WorkOrder");

            url = "https://centraldeservicos-is.onbmc.com/api/arsys/v1/entry/WOI:WorkOrder";
            jsonBody = JsonSerializer.Serialize(req);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de chamado inválido");
        }

        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        // Aqui você ajusta conforme o formato de retorno da API BMC
        var dados = JsonSerializer.Deserialize<List<ChamadoResponseDto>>(responseJson);

        return dados ?? new List<ChamadoResponseDto>();
	}
	// public async Task<string> GetChamadoByNumberAsync(string chamadoNumber)
	// {
	// 	var token = await LoginAsync();
	// 	_httpClient.DefaultRequestHeaders.Authorization =
	// 		new AuthenticationHeaderValue("AR-JWT", token);



	// 	var response = await _httpClient.PostAsJsonAsync(
	// 		"https://centraldeservicos-is.onbmc.com/api/arsys/v1/entry/HPD:IncidentInterface", chamadoNumber);
	// 	//  return await _httpClient.GetFromJsonAsync<ChamadoDto>($"https://centraldeservicos-is.onbmc.com/api/arsys/v1/entry/HPD:IncidentInterface?q=('Incident Number'= \"{chamadoNumber}\")");
	// 		// HPD:IncidentInterface?q=('Incident Number'= "INC000000011430")

	// 	response.EnsureSuccessStatusCode();
	// 	return await response.Content.ReadAsStringAsync();
	// }

	// public async Task<string> CriarWorkOrderAsync(WorkOrderDto dto)
	// {
	// 	var token = await LoginAsync();
	// 	_httpClient.DefaultRequestHeaders.Authorization =
	// 		new AuthenticationHeaderValue("AR-JWT", token);

	// 	var response = await _httpClient.PostAsJsonAsync(
	// 		"https://centraldeservicos-is.onbmc.com/api/arsys/v1/entry/WOI:WorkOrder", dto);

	// 	response.EnsureSuccessStatusCode();
	// 	return await response.Content.ReadAsStringAsync();
	// }
}
