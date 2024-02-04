using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velpoint : MonoBehaviour
{
    public Rigidbody satellite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = satellite.velocity;
    }
}
