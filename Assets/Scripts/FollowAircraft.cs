using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAircraft : MonoBehaviour {
    public Vector3 offset;
    public GameObject aircraftInstance;

    void LateUpdate() {
        transform.position = aircraftInstance.transform.position + offset;
    }
}
