namespace AircraftSimulator.Physics.MariaRybakonenko
{
    public struct MariaRybakonenkoModelData
    {
        public float DeadZone;
        public float ControlRate;
        public float Lerp;

        public float MaxTurn;
        public float AileronTurnRate;
        public float ElevatorTurnRate;
        public float RudderTurnRate;
        public float WindTurnRate;
    }
}