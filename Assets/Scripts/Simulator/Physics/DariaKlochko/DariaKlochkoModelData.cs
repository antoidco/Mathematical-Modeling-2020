namespace AircraftSimulator.Physics.DariaKlochko{
    public struct DariaKlochkoModelData
    {
        public float DeadZone;
        public float ControlRate;
        public float Lerp;

        public float MaxTurn;
        public float AileronTurnRate;
        public float ElevatorTurnRate;
        public float RudderTurnRate;

        public Forsage Forsage;
    }
}