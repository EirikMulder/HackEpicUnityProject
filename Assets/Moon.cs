using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moon : MonoBehaviour
{
    public Vector3 initialPosition;
    public Rigidbody rigidBody;
    public static float G = 6.67408e-11f;
    public static float massJupiter = 1.89813e27f;
    public float systemScale;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        float velocityMagnitude = Mathf.Sqrt(G * massJupiter * systemScale / transform.position.magnitude);
        Vector3 initialVelocity = Vector3.Cross(transform.position, Vector3.up);
        initialVelocity = initialVelocity.normalized * velocityMagnitude;
        rigidBody.velocity = initialVelocity;
        // Vector3
        // Rigidbody rigidBody = GetComponent<Rigidbody>();
        // rigidBody.angularVelocity = new Vector3(0, 20, 0);
    }

    private void Update()
    {
        // Debug.Log(Input.GetKey("down"));
    }

    private void FixedUpdate()
    {

        Vector3 unitR = transform.position.normalized;
        float rMag = transform.position.magnitude;

        Vector3 force = G * rigidBody.mass * massJupiter * systemScale / (rMag * rMag) * -unitR;


        rigidBody.AddForce(force);
    }
}
