namespace AuthApi.Entities;

public record AuthenticationToken(string Token, int ExpiresIn);
