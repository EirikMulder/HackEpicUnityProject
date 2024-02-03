using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public float G = 6.67408e-11f;
    public float massJupiter = 1.89813e27f;
    public float systemScale = 1e-15f;

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
}
