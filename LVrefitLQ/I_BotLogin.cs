using Refit;

namespace LVrefitLOLP
{
    internal interface I_BotLogin
    {
        [Post("/api.php")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<ApiResponse<string>> BotLoginPost(
            [Body(BodySerializationMethod.UrlEncoded)] BotLoginParams botLoginParams,
            [AliasAs("lgname")] string username,
            [AliasAs("lgdomain")] string? loginUrl = null,
            //coto login return url? 
            [AliasAs("action")] string action = "login",
            [AliasAs("format")] string format = "json"

            );
    }
}
