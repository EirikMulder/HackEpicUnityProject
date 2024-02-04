using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelect : MonoBehaviour
{
    public Camera front_camera;
    public Camera pers_camera;
    public Camera orb_camera;
    
    public TrailRenderer sat_trail;

    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        pers_camera.enabled = true;
        front_camera.enabled = false;
        orb_camera.enabled = false;

        sat_trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { i++; }
        if (i == 0) 
        {
            pers_camera.enabled = true;
            front_camera.enabled = false;
            orb_camera.enabled = false;
        }
        else if (i == 1)
        {
            pers_camera.enabled = false;
            front_camera.enabled = true;
            orb_camera.enabled = false;
        }
        else if (i == 2)
        {
            pers_camera.enabled = false;
            front_camera.enabled = false;
            orb_camera.enabled = true;
        }
        else { i = 0; }

        if (Input.GetKeyDown(KeyCode.RightShift)) { sat_trail.enabled = !sat_trail.enabled; }
        if (Input.GetKeyDown(KeyCode.RightControl)) { sat_trail.Clear(); }
    }
}
