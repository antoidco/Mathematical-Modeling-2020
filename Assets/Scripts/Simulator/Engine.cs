using System;
using UnityEngine;

namespace AircraftSimulator
{
    public class Engine : Component
    {
        private double _currentPower;

        public Engine(double mass, string name, Vector3 relativePosition, double maxPower = 100)
        {
            _currentPower = 0;
            MaxPower = maxPower;
            Mass = mass;
            Name = name;
            RelativePosition = relativePosition;
        }

        public virtual double CurrentPower
        {
            get => _currentPower;
            set => _currentPower = Math.Max(Math.Min(MaxPower, value), 0);
        }

        public double MaxPower { get; set; }
    }
}