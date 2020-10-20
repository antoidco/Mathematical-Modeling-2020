using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHouses : MonoBehaviour {
    public int range1 = 2000;
    public int range2 = 2000;
    public int count = 250;
    public List<GameObject> prefabs;
    public GameObject houses;
    void Start()
    {
        for (int i = 0; i < count; ++i) {
            var house = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)], houses.transform);
            house.transform.position = new Vector3(Random.Range(-range1, range1), Random.Range(-range2, range2), 0);
        }
    }
}
