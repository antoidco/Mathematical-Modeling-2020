using System;
using System.Numerics;

namespace PlaneSim.PlaneSim {
    public class Engine : AComponent {
        private double _currentPower;

        public double CurrentPower {
            get { return _currentPower; }
            set { _currentPower = Math.Max(Math.Min(MaxPower, value), 0); }
        }

        public double MaxPower { get; set; }

        public Engine(double mass, string name, Vector3 relativePosition, double maxPower = 100) {
            _currentPower = 0;
            MaxPower = 100;
            Mass = mass;
            Name = name;
            RelativePosition = relativePosition;
        }
    }
}