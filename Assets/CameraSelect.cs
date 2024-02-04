using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelect : MonoBehaviour
{
    public Camera front_camera;
    public Camera pers_camera;
    
    public TrailRenderer sat_trail;
    // Start is called before the first frame update
    void Start()
    {
        pers_camera.enabled = true;
        front_camera.enabled = false;

        sat_trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            pers_camera.enabled = !pers_camera.enabled;
            front_camera.enabled = !front_camera.enabled;
        }

        if (Input.GetKeyDown(KeyCode.RightShift)) { sat_trail.enabled = !sat_trail.enabled; }
    }
}
