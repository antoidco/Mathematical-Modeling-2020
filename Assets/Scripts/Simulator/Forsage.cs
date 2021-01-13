using System;
using UnityEngine;

namespace AircraftSimulator {
    public class Forsage : MonoBehaviour {
        public bool IsActive = false;    
        public void ButtonForsage() {
            IsActive = true;
            Debug.Log("Forsage!");
        }
    }
}