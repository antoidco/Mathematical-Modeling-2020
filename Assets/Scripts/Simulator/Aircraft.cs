using System.Collections.Generic;
using UnityEngine;

namespace AircraftSimulator {
    public class Aircraft {
        public Rotation Rotation { get; set; }
        public Vector3 Position { get; set; }
        public List<Component> Components;

        public Aircraft(Vector3 initialPosition, Quaternion quaternion) {
            Rotation = new Rotation(quaternion);
            Components = new List<Component>();
            Position = initialPosition;
        }

        public double Mass => EvaluateMass();

        private double EvaluateMass() {
            double sum = 0;
            foreach (var component in Components) sum += component.Mass;

            return sum;
        }
    }
}