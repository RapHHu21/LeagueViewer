using Refit;

namespace LVrefitLOLP
{
    public interface I_LoginToken
    {
        [Get("/api.php")]
        Task<ApiResponse<string>> LoginToken(
            [Query] string action = "query",
            [Query] string meta = "tokens",
            [Query] string type = "login",
            [Query] string format = "json"
            );
    }
}
