using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Rigidbody satellite;

    private float sensitivity = 2.5f;
    private float range = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = satellite.position + Vector3.Cross(satellite.velocity * range, satellite.position);
        transform.forward = Vector3.Cross(satellite.position, satellite.velocity);

        range = range + (Input.GetAxis("Mouse ScrollWheel") * sensitivity);
    }
}
