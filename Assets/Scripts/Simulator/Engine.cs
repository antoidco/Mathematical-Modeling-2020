using System;
using UnityEngine;

namespace AircraftSimulator {
    public class Wind1 : Component {
        private double _currentWind;

        public Wind1(double mass, string name, Vector3 relativePosition, double maxPower = 100) {
            _currentWind = 0;
            MaxWind = 400;
            Mass = mass;
            Name = name;
            RelativePosition = relativePosition;
        }

        public double CurrentWind {
            get => _currentWind;
            set => _currentWind = Math.Max(Math.Min(MaxWind, value), 0);
        }

        public double MaxWind { get; set; }
    }
}