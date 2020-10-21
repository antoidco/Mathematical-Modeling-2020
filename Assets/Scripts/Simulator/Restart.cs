using UnityEngine;

namespace AircraftSimulator {
    public class Restart : MonoBehaviour {
        private Simulator _simulator;
        void Start() {
            _simulator = FindObjectOfType<SimulatorComponent>().GetSimulator();
        }

        public void OnRestart() {
            _simulator.Restart(300);
        }
    }
}
