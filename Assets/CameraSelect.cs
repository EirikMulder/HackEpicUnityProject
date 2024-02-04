using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelect : MonoBehaviour
{
    public Camera front_camera;
    public Camera pers_camera;
    public Camera orb_camera;
    
    public TrailRenderer sat_trail;

    private int cam_select = 0;
    private int j = 0;
    public float ts = 0;

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
        if (pausegame.pause) return;

        if (Input.GetKeyDown(KeyCode.Return)) { cam_select++; }
        if (cam_select == 0)
        {
            pers_camera.enabled = true;
            front_camera.enabled = false;
            orb_camera.enabled = false;
        }
        else if (cam_select == 1)
        {
            pers_camera.enabled = false;
            front_camera.enabled = true;
            orb_camera.enabled = false;
        }
        else if (cam_select == 2)
        {
            pers_camera.enabled = false;
            front_camera.enabled = false;
            orb_camera.enabled = true;
        }
        else { cam_select = 0; }

        if (Input.GetKeyDown(KeyCode.RightShift)) { sat_trail.enabled = !sat_trail.enabled; }
        if (Input.GetKeyDown(KeyCode.RightControl)) { sat_trail.Clear(); }
    }
}
