namespace SurfSpotAPI.Models
{
    public class SurfSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Info { get; set; }
        public string BestMonthsToSurf { get; set; }
        public string BestTideToSurf { get; set; }
        public string Dangers { get; set; }
        public string WaveLength { get; set; }
        public string WaveDirection { get; set; }

        // Constructor
        public SurfSpot(int id, string name, double latitude, double longitude, string info,
                        string bestMonthsToSurf, string bestTideToSurf, string dangers,
                        string waveLength, string waveDirection)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Info = info;
            BestMonthsToSurf = bestMonthsToSurf;
            BestTideToSurf = bestTideToSurf;
            Dangers = dangers;
            WaveLength = waveLength;
            WaveDirection = waveDirection;
        }

        public SurfSpot()
        {
        }

        // Override ToString() method to provide a custom string representation of the object
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Latitude: {Latitude}, Longitude: {Longitude}, Info: {Info}, " +
                   $"Best Months to Surf: {BestMonthsToSurf}, Best Tide to Surf: {BestTideToSurf}, " +
                   $"Dangers: {Dangers}, Wave Length: {WaveLength}, Wave Direction: {WaveDirection}";
        }
    }
}
