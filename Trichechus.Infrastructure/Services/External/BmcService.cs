using System.Net.Http.Json;
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
			Username = _config["BMC:Username"],
			Password = _config["BMC:Password"]
		};

		var response = await _httpClient.PostAsJsonAsync(
			"https://centraldeservicos-is.onbmc.com/api/rx/authentication/loginrequest", login);

		response.EnsureSuccessStatusCode();

		var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
		return result?.Token ?? throw new Exception("Token não retornado");
	}

	// public async Task<string> CriarIncidenteAsync(IncidenteDto dto)
	// {
	// 	var token = await LoginAsync();
	// 	_httpClient.DefaultRequestHeaders.Authorization =
	// 		new AuthenticationHeaderValue("AR-JWT", token);

	// 	var response = await _httpClient.PostAsJsonAsync(
	// 		"https://centraldeservicos-is.onbmc.com/api/arsys/v1/entry/HPD:IncidentInterface", dto);

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
