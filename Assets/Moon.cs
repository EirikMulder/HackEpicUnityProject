using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moon : MonoBehaviour
{
    public Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        float velocityMagnitude = Mathf.Sqrt(SceneController.Instance.G * SceneController.Instance.massJupiter * SceneController.Instance.systemScale / transform.position.magnitude);
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

        Vector3 force = SceneController.Instance.G * rigidBody.mass * SceneController.Instance.massJupiter * SceneController.Instance.systemScale / (rMag * rMag) * -unitR;

        rigidBody.AddForce(force);
    }
}
