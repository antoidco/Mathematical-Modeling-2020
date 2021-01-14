using System;
using System.Collections;
using UnityEngine;

namespace AircraftSimulator {
    public class Forsage : MonoBehaviour {
        public bool IsActive = false;
        IEnumerator DisableAfterOneSecond(){
            yield return new WaitForSeconds(1f); // here we wait for 1 second
            IsActive = false; // here we disable forsage
            Debug.Log("Forsage disabled");
        }
        public void ButtonForsage() {
            IsActive = true;
            Debug.Log("Forsage!");
            
            StartCoroutine("DisableAfterOneSecond");
        }
    }
}