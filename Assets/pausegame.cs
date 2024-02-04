using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausegame : MonoBehaviour
{
    public static bool pause;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { pause = !pause; }

        if (pause) { Time.timeScale = 0; }
    }
}
