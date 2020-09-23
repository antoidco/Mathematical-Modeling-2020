using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PlaneSim.PlaneSim
{
    class Aircraft
    {
        public Vector3 Position { get; set; }
        public List<AircraftComponent> Components;
        public double Mass
        {
            get
            {
                return getMass();
            }
        }

        public Aircraft(Vector3 initialPosition)
        {
            Components = new List<AircraftComponent>();
            Position = initialPosition;
        }

        private double getMass()
        {
            double sum = 0;
            foreach (var component in Components)
            {
                sum += component.Mass;
            }
            return sum;
        }
    }
}
