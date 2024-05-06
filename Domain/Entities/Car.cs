namespace Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string? TeamName { get; set; }
        public int Speed { get; set; }
        public double MalfunctionChance { get; set; }
        public int MalfunctionsOcurred { get; set; }
        public int DistanceCoveredInMiles { get; set; }
        public bool FinishedRace { get; set; }
        public int RacedForHours { get; set; }
    }
}
