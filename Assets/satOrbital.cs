using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class satOrbital : MonoBehaviour
{

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        var G = SceneController.Instance.G;
        var massJupiter = SceneController.Instance.massJupiter;
        var systemScale = SceneController.Instance.systemScale;

        rigidBody = GetComponent<Rigidbody>();
        float velocityMagnitude = Mathf.Sqrt(G * massJupiter / transform.position.magnitude / systemScale);
        velocityMagnitude *= systemScale * systemScale;
        Vector3 initialVelocity = Vector3.Cross(transform.position, Vector3.up);
        initialVelocity = initialVelocity.normalized * velocityMagnitude;
        rigidBody.velocity = initialVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var G = SceneController.Instance.G;
        var massJupiter = SceneController.Instance.massJupiter;
        var systemScale = SceneController.Instance.systemScale;

        Vector3 unitR = transform.position.normalized;
        float rMag = transform.position.magnitude / systemScale;

        Vector3 force = G * rigidBody.mass * massJupiter / (rMag * rMag) * -unitR;
        force *= systemScale;

        Vector3 force2 = SceneController.Instance.CalculateAcceleration(transform.position) * rigidBody.mass;

        rigidBody.AddForce(force);
        rigidBody.AddForce(force2);

        // Debug.Log($"Jupiter: {force.magnitude}");
        // Debug.Log($"Europa: {force2.magnitude}");
    }
}
