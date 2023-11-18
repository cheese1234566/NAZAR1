using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public GameObject[] spawnPlaces;
    public GameObject key;

    private void Awake()
    {
        
        int RandomPlace = Random.Range(0, spawnPlaces.Length);
        Instantiate(key, spawnPlaces[RandomPlace].transform.position, Quaternion.identity);
    }
}
