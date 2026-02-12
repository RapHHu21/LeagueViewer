using Refit;

namespace LVrefitLOLP
{
    public interface I_CargoQueryTest
    {
        [Get("/api.php")]
        Task<ApiResponse<string>> CargoQueryTests(
            [AliasAs("tables")] string tables,
            [AliasAs("fields")] string fields,
            [AliasAs("join_on")] string join_on,
            [AliasAs("group_by")] string? group_by = null,
            [AliasAs("order_by")] string? order_by = null,
            [AliasAs("action")] string action = "cargoquery",
            [AliasAs("format")] string format = "json",
            [AliasAs("offset")] int? offset = null,
            [AliasAs("limit")] int limit = 10,
            [AliasAs("where")] string? where = ""
            );
    }
}
