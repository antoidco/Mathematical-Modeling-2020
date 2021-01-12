using System;
using UnityEngine;

namespace AircraftSimulator {
    public class Engine : Component  {
        private double _currentPower;
        AircraftSimulator.Forsage Forsage = new AircraftSimulator.Forsage();
        public Engine(double mass, string name, Vector3 relativePosition, double maxPower = 100) {
            _currentPower = 0;
            MaxPower = 400;
            Mass = mass;
            Name = name;
            RelativePosition = relativePosition;
        }

        public double CurrentPower {
            get => _currentPower;
            set {
                if (Forsage.forsage == false) {
                    _currentPower = Math.Max(Math.Min(MaxPower, value), 0);
                }
                else {
                    _currentPower = 5 * Math.Max(Math.Min(MaxPower, value), 0);
                }
            }
            /* set => _currentPower = Math.Max(Math.Min(MaxPower, value), 0);*/
        }
        public double MaxPower { get; set; }
    }
}