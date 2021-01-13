using System;
using System.Collections;
using UnityEngine;

namespace AircraftSimulator {
    public class Forsage : MonoBehaviour {
        public bool IsActive = false;
        
        /* here we need to add some code to disable Forsage after 1 second, for example
         * in Unity it is some kind of tricky, you need to use Coroutines in order to "wait" in background
         * https://docs.unity3d.com/Manual/Coroutines.html
         * please, read this article and try to complete the code
        */
        
        // this is coroutine
        IEnumerator DisableAfterOneSecond() 
        {
            yield return new WaitForSeconds(1f); // here we wait for 1 second
            IsActive = false; // here we disable forsage
            Debug.Log("Forsage disabled");
        }
        public void ButtonForsage() {
            IsActive = true;
            Debug.Log("Forsage!");
        
            /*past your code here*/.("DisableAfterOneSecond");
        }
    }
}