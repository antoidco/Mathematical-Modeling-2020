namespace AircraftSimulator {
    public class Weather {
        public Wind Wind { get; private set; }

        public Weather(Wind wind) {
            Wind = wind;
        }
    }
}