
namespace LVrefitLOLP
{   
    public class DeserializedCargoNamesSplit()
    {
        public DeserializeCargoNames? DeserializeCargoNames { get; set; }
    }

    public class DeserializeCargoNames
    {
        public string? header { get; set; }
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
