using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlaneSim.PlaneSim {
    class Aircraft {
        public Rotation Rotation { get; set; }
        public Vector3 Position { get; set; }
        public List<AComponent> Components;

        public double Mass {
            get { return getMass(); }
        }

        public Aircraft(Vector3 initialPosition, Rotation rotation) {
            Rotation = rotation;
            Components = new List<AComponent>();
            Position = initialPosition;
        }

        private double getMass() {
            double sum = 0;
            foreach (var component in Components) {
                sum += component.Mass;
            }

            return sum;
        }
    }
}