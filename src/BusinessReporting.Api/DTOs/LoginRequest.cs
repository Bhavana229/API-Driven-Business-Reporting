namespace BusinessReporting.Api.DTOs;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string AccessToken, DateTime ExpiresAtUtc, string Role);
