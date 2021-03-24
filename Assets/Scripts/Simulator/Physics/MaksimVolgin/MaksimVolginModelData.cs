namespace AircraftSimulator.Physics.Basic
{
    public struct MaksimVolginModelData
    {
        public float DeadZone;
        public float ControlRate;
        public float Lerp;

        public float MaxTurn;
        public float AileronTurnRate;
        public float ElevatorTurnRate;
        public float RudderTurnRate;

        public float aerodynamicDrag;
        public float frontalResistanceArea;
        public float airDensity;
    }
}
