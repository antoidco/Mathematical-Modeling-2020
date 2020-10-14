using System.Collections.Generic;
using System.Numerics;

namespace AircraftSimulator {
    public class Aircraft {
        public Rotation Rotation { get; set; }
        public Vector3 Position { get; set; }
        public List<Component> Components;

        public double Mass {
            get { return EvaluateMass(); }
        }

        public Aircraft(Vector3 initialPosition, Rotation rotation) {
            Rotation = rotation;
            Components = new List<Component>();
            Position = initialPosition;
        }

        private double EvaluateMass() {
            double sum = 0;
            foreach (var component in Components) {
                sum += component.Mass;
            }

            return sum;
        }
    }
}