using System.Numerics;

namespace AircraftSimulator {
    public abstract class Component {
        public double Mass { get; set; }
        public string Name { get; set; }
        public Vector3 RelativePosition { get; set; }
    }
}