using System;
using System.Collections;
using UnityEngine;

namespace AircraftSimulator {
    public class Forsage : MonoBehaviour {
        public int j = 1;
        public int k = 1;
        public bool isDisactive = false;
        public bool IsActive = false;
        IEnumerator DisableAfterTwoSeconds()
        {
            while (k <= 25)
            {


                yield return new WaitForSeconds(0.1f); // here we wait for 0.1 second


                k++;
            }


            isDisactive = false;
             Debug.Log("Forsage disabled");

        }
        IEnumerator DisableAfterOneSecond(){
            while (j <= 25)
            {
               
                
                yield return new WaitForSeconds(0.1f); // here we wait for 0.1 second

                 
            j++;
            }
            IsActive = false; // here we disable forsage
            isDisactive = true;
            StartCoroutine("DisableAfterTwoSeconds");
           

        }
            
        public void ButtonForsage() {
            Debug.Log("Forsage!");
            IsActive = true;


            StartCoroutine("DisableAfterOneSecond");


        }
    }
}
