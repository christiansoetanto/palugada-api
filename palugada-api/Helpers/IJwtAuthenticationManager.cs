namespace palugada_api.Helpers {
    public interface IJwtAuthenticationManager {
        string GenerateToken(string username);
    }
}
