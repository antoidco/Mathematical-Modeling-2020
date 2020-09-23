using PlaneSim.PlaneSim;
using System;
using System.Numerics;

namespace PlaneSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(getPlaneMassExample());
            Console.ReadLine();

            simulationExample(0.1f);
        }

        private static double getPlaneMassExample()
        {
            var aircraft = new Aircraft(new Vector3(0, 0, 1000));
            var wingComponent = new AircraftComponent();
            wingComponent.Mass = 5;
            wingComponent.Name = "Left wing";
            aircraft.Components.Add(wingComponent);

            aircraft.Components.Add(new AircraftComponent() { Mass = 10, Name = "Right wing" });
            aircraft.Components.Add(new AircraftComponent() { Mass = 50, Name = "Engine"});

            return aircraft.Mass;
        }

        private static void simulationExample(float timeStep)
        {
            var aircraft = new Aircraft(new Vector3(0, 0, 250));
            var simulator = new AircraftSimulator(aircraft);

            while (aircraft.Position.Z > 0)
            {
                simulator.Update(timeStep);
                Console.WriteLine(aircraft.Position.ToString());
            }
            Console.ReadLine();
        }
    }
}
