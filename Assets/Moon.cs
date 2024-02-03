using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moon : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 initialVelocity;
    public Rigidbody rigidBody;
    public static float G = 6.67408e-11f;
    public static float massJupiter = 1.89813e27f;
    public float systemScale;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        // Rigidbody rigidBody = GetComponent<Rigidbody>();
        // rigidBody.angularVelocity = new Vector3(0, 20, 0);
    }

    private void Update()
    {

        Vector3 newRotation = transform.rotation.eulerAngles + new Vector3(0, 10 / 360f * 2*3.1415f, 0);

        // transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(newRotation));

        Vector3 unitR = transform.position.normalized / systemScale;
        float rMag = transform.position.magnitude / systemScale;

        Vector3 force = G * rigidBody.mass * massJupiter / (rMag * rMag) * unitR;


        rigidBody.AddForce(force);
    }
}
