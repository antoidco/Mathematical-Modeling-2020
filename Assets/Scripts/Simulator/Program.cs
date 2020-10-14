using System;
using UnityEngine;

namespace AircraftSimulator {
    internal class Program {
        /*
        static void Main(string[] args) {
            Console.WriteLine(GetPlaneMassExample());
            Console.ReadLine();

            SimulationExample(0.1f);
        }
        */

        private static double GetPlaneMassExample() {
            var aircraft = new Aircraft(new Vector3(0, 0, 1000), new Rotation());

            return aircraft.Mass;
        }

        private static void SimulationExample(float timeStep) {
            var aircraft = new Aircraft(new Vector3(0, 0, 250), new Rotation());
            var engine1 = new Engine(10, "Engine 1", Vector3.zero);
            aircraft.Components.Add(engine1);
            engine1.CurrentPower = 40;
            var simulator =
                new Simulator(aircraft, new Weather(new Wind(new ConstantWindModel(new Vector3(1, 2, 3))))); // lol

            while (aircraft.Position.z > 0) {
                simulator.Update(timeStep);
                Console.WriteLine(aircraft.Position.ToString());
            }

            Console.ReadLine();
        }
    }
}