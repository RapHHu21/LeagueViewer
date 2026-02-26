using Refit;

namespace LVrefitLOLP
{
    public interface I_CargoMatches
    {
        //base querry for matches
        [Get("/api.php")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<ApiResponse<string>> CargoMatches(
            [AliasAs("where")] string? where,
            [AliasAs("group_by")] string? group_by = null,
            [AliasAs("order_by")] string? order_by = null,
            [AliasAs("limit")] int limit = 10,
            [AliasAs("tables")] string tables = "ScoreboardGames=SG, Tournaments=T",
            [AliasAs("fields")] string fields = "T.Name, SG.DateTime_UTC, SG.Team1, SG.Team2",
            [AliasAs("join_on")] string join_on = "SG.OverviewPage=T.OverviewPage",
            [AliasAs("offset")] int? offset = null,
            [AliasAs("action")] string action = "cargoquery",
            [AliasAs("format")] string format = "json"
            );
    }
}
