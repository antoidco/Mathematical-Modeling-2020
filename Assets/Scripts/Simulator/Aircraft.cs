using System.Collections.Generic;
using UnityEngine;

namespace AircraftSimulator
{
    public class Aircraft
    {
        public List<Component> Components;
        public double DragConstant;
        public Vector3 Inertia;

        public Aircraft(Vector3 initialPosition, Quaternion quaternion, double dragConstant,
            Vector3 inertia)
        {
            Rotation = new Rotation(quaternion);
            Components = new List<Component>();
            Position = initialPosition;
            DragConstant = dragConstant;
            Inertia = inertia;
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