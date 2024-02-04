using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public float G = 6.67408e-11f;
    public float massJupiter
    {
        get => _massJupiter * timeScale;
    }
    private float _massJupiter = 1.89813e27f;
    public float systemScale = 1e-15f;
    public float timeScale;

    public List<Moon> attractors = new();

    public static SceneController Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public Vector3 CalculateAcceleration(Vector3 position)
    {
        Vector3 accel = Vector3.zero;
        foreach (var attractor in attractors)
        {
            Vector3 positionOffset = position - attractor.transform.position;
            Vector3 unitR = positionOffset.normalized;
            float rMagnitude = positionOffset.magnitude;

            accel += G * attractor.mass * systemScale / (rMagnitude * rMagnitude) * -unitR;
        }
        Instance = this;

        return accel;
    }
}
