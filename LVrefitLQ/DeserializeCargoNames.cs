
namespace LVrefitLOLP
{
    public class DeserializeCargoNames
    {
        public List<TitleField>? cargoquery { get; set; }

    }

    public class TitleField
    {
        public Title? title {  get; set; }
    }

    public class Title
    {
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? TournamentLevel { get; set; }
        public string? Date { get; set; }
        public string? DateStart { get; set; }
    }
}
