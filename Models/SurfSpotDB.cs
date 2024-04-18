using Microsoft.Extensions.Logging;

namespace SurfSpotAPI.Models
{
    public class SurfSpotsDB
    {
        public static List<SurfSpot> SurfSpotsList = new List<SurfSpot>()
        {
            new SurfSpot(1, "Pipeline", 21.6657, -158.0554, "Pipeline, located on the North Shore of Oahu, Hawaii, is famous for its powerful and barreling waves.", "November to February", "Mid to high tide", "Reef hazards, strong currents", "Short", "Left"),
            new SurfSpot(2, "Jeffreys Bay", -34.0407, 24.9214, "Jeffreys Bay, South Africa, is renowned for its long, fast, and tubing right-hand point break.", "April to September", "Low to mid tide", "Sharks, strong rips", "Long", "Right"),
            new SurfSpot(3, "Uluwatu", -8.8057, 115.1056, "Uluwatu, located in Bali, Indonesia, offers consistent and challenging waves, particularly at its famous left-hand reef break.", "April to October", "Mid to high tide", "Reef hazards, strong currents", "Long", "Left"),
            new SurfSpot(4, "Banzai Pipeline", 21.6624, -158.0561, "Banzai Pipeline, also known simply as Pipeline, is one of the most famous surf breaks in the world, located on the North Shore of Oahu, Hawaii.", "December to February", "Mid to high tide", "Shallow reef, strong currents", "Short", "Left"),
            new SurfSpot(5, "Teahupo'o", -17.8492, -149.8992, "Teahupo'o, located on the southwest coast of Tahiti in French Polynesia, is known for its heavy and powerful waves, often considered one of the most challenging breaks in the world.", "May to August", "Mid to high tide", "Reef hazards, shallow water", "Short to long", "Left"),
            new SurfSpot(6, "Trestles", 33.3836, -117.5931, "Trestles, located in San Clemente, California, USA, is a world-class surf spot known for its consistent and high-quality waves, suitable for all levels of surfers.", "May to September", "Mid tide", "Crowds, occasional pollution", "Medium", "Right"),
            new SurfSpot(7, "Superbank", -28.0963, 153.4388, "The Superbank, located in Gold Coast, Queensland, Australia, is famous for its long and perfectly formed waves, offering some of the best conditions for high-performance surfing.", "March to October", "Mid tide", "Crowds, strong rips", "Long", "Right"),
            new SurfSpot(8, "Rincon", 34.3768, -119.485, "Rincon, located near Santa Barbara, California, USA, is a classic point break known for its long, peeling waves and consistent surf conditions.", "November to March", "Mid tide", "Crowds, rocks", "Long", "Right"),
            new SurfSpot(9, "Hossegor", 43.6805, -1.4447, "Hossegor, located in southwestern France, is known for its powerful beach breaks and is considered one of Europe's best surf destinations.", "September to November", "Mid tide", "Crowds, strong currents", "Medium", "Left"),
            new SurfSpot(10, "Huntington Beach", 33.6595, -118.0019, "Huntington Beach, also known as Surf City USA, is located in California and is famous for its consistent surf and vibrant surf culture.", "June to August", "Mid tide", "Crowds, pollution", "Medium", "Right"),
            new SurfSpot(11, "Jaws (Peahi)", 20.935, -156.2997, "Jaws, also known as Peahi, is located on the north shore of Maui, Hawaii, and is famous for its massive waves, attracting big wave surfers from around the world.", "December to February", "N/A", "Reef hazards, strong currents", "Very long", "Right"),
            new SurfSpot(12, "Cloudbreak", -17.7789, 177.1727, "Cloudbreak, located in Fiji's Tavarua Island, is a world-renowned reef break known for its powerful and consistent waves, suitable for experienced surfers.", "April to October", "Mid tide", "Reef hazards, strong currents", "Long", "Left"),
            new SurfSpot(13, "Mavericks", 37.4955, -122.5021, "Mavericks is a famous big wave surf spot located near Half Moon Bay in Northern California, USA, known for its massive swells and challenging conditions.", "November to March", "N/A", "Reef hazards, strong currents", "Very long", "Right"),
            new SurfSpot(14, "Lower Trestles", 33.3815, -117.5996, "Lower Trestles, located in San Clemente, California, USA, is a high-performance wave famous for its long rides and consistent shape.", "May to September", "Mid tide", "Crowds, environmental concerns", "Medium", "Right"),
            new SurfSpot(15, "Bells Beach", -38.3664, 144.2849, "Bells Beach, located in Victoria, Australia, is famous for its powerful swells and iconic Easter surfing competition, the Rip Curl Pro.", "March to May", "Mid tide", "Rocks, strong rips", "Long", "Right"),
            new SurfSpot(16, "Mundaka", 43.4074, -2.6953, "Mundaka, located in the Basque Country of Spain, is famous for its long left-hand barrel wave, considered one of the best rivermouth waves in the world.", "September to November", "Mid tide", "Shallow sandbanks, crowds", "Long", "Left"),
            new SurfSpot(17, "Manu Bay", -38.2855, 174.8761, "Manu Bay, located near Raglan on the North Island of New Zealand, is known for its long, consistent left-hand point break, suitable for surfers of all levels.", "All year", "Mid tide", "Rocks, strong rips", "Long", "Left"),
            new SurfSpot(18, "Supertubos", 39.3599, -9.3641, "Supertubos, located in Peniche, Portugal, is known for its powerful and hollow waves, often hosting world-class surfing competitions.", "September to April", "Mid tide", "Rocks, strong currents", "Medium", "Right"),
            new SurfSpot(19, "Manly Beach", -33.7996, 151.2897, "Manly Beach, located in Sydney, Australia, is a popular surf destination with consistent beach breaks suitable for surfers of all levels.", "All year", "Mid tide", "Crowds, pollution", "Short to medium", "Both"),
            new SurfSpot(20, "Nazare", 39.6012, -9.0736, "Nazare, located on the coast of Portugal, is famous for its massive waves, attracting big wave surfers from around the world.", "October to March", "N/A", "Massive waves, strong currents", "Very long", "Right"),
        };
        public List<SurfSpot> getSurfSpotList()
        {
            return SurfSpotsList;
        }

    }
}
