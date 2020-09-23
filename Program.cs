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
            var aircraft = new Aircraft(new Vector3(0, 0, 1000), new Rotation());

            return aircraft.Mass;
        }

        private static void simulationExample(float timeStep)
        {
            var aircraft = new Aircraft(new Vector3(0, 0, 250), new Rotation());
            var engine1 = new Engine(10, "Engine 1", Vector3.Zero);
            aircraft.Components.Add(engine1);
            engine1.CurrentPower = 40;
            var simulator = new Simulator(aircraft);

            while (aircraft.Position.Z > 0)
            {
                simulator.Update(timeStep);
                Console.WriteLine(aircraft.Position.ToString());
            }
            Console.ReadLine();
        }
    }
}
