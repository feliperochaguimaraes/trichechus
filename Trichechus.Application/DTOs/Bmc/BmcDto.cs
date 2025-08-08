using System.ComponentModel.DataAnnotations;

namespace Trichechus.Application.DTOs.Bmc;

public class LoginRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginResponse
{
    public string Token { get; set; } = default!;
}