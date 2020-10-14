using System.Collections.Generic;
using UnityEngine;

//using System.Numerics;

namespace AircraftSimulator
{
    internal class Aircraft
    {
        public List<Component> Components;

        public Aircraft(Vector3 initialPosition, Rotation rotation)
        {
            Rotation = rotation;
            Components = new List<Component>();
            Position = initialPosition;
        }

        public Rotation Rotation { get; set; }
        public Vector3 Position { get; set; }

        public double Mass => EvaluateMass();

        private double EvaluateMass()
        {
            double sum = 0;
            foreach (var component in Components) sum += component.Mass;

            return sum;
        }
    }
}