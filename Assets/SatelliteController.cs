using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SayHello("Kody");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SayHello(string name)
    {
        Debug.Log(name);
    }
}
