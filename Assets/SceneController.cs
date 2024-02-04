using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public float G = 6.67408e-11f;
    // public float massJupiter
    // {
    //     get => _massJupiter * timeScale;
    // }
    public float massJupiter = 1.89813e27f;
    public float systemScale = 1e-15f;
    public float timeScale;

    public List<Moon> attractors = new();
    public List<Tuple<Vector3, float>> attractorTuples => attractors.Select(i => new Tuple<Vector3, float>(i.transform.position, i.mass)).ToList();

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

    public Vector3 CalculateAcceleration(Vector3 position, List<Tuple<Vector3, float>> attractorTuples)
    {
        Vector3 accel = Vector3.zero;
        foreach (var attractor in attractorTuples)
        {
            Vector3 positionOffset = position - attractor.Item1;
            Vector3 unitR = positionOffset.normalized;
            float rMagnitude = positionOffset.magnitude / systemScale;

            accel += G * attractor.Item2 / (rMagnitude * rMagnitude) * -unitR;
        }

        // Jupiter Makes you Stupider
        Vector3 unitRJupiter = position.normalized;
        float rMag = position.magnitude / systemScale;

        Vector3 jupiterAccel = G * massJupiter / (rMag * rMag) * -unitRJupiter;
        accel += jupiterAccel;

        return accel * (systemScale * timeScale);
    }
}
