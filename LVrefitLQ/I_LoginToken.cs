using Refit;

namespace LVrefitLOLP
{
    public interface I_LoginToken
    {
        [Get("/api.php")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<ApiResponse<string>> LoginToken(
            [Query] string action = "query",
            [Query] string meta = "tokens",
            [Query] string type = "login",
            [Query] string format = "json"
            );
    }
}
