using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Moon : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float mass;

    void Start()
    {
        var G = SceneController.Instance.G;
        var massJupiter = SceneController.Instance.massJupiter;
        var systemScale = SceneController.Instance.systemScale;

        rigidBody = GetComponent<Rigidbody>();
        float velocityMagnitude = Mathf.Sqrt(SceneController.Instance.G * SceneController.Instance.massJupiter / transform.position.magnitude / SceneController.Instance.systemScale);
        velocityMagnitude *= systemScale * systemScale;
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
        var G = SceneController.Instance.G;
        var massJupiter = SceneController.Instance.massJupiter;
        var systemScale = SceneController.Instance.systemScale;

        Vector3 unitR = transform.position.normalized;
        float rMag = transform.position.magnitude / systemScale;

        Vector3 force = SceneController.Instance.G * rigidBody.mass * SceneController.Instance.massJupiter / (rMag * rMag) * -unitR;
        force *= systemScale;

        rigidBody.AddForce(force);
    }
}
