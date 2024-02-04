using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelect : MonoBehaviour
{
    public Camera front_camera;
    public Camera pers_camera;
    public Camera orb_camera;
    
    public TrailRenderer sat_trail;
    public LineRenderer forw_trail;

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
        forw_trail.enabled = false;
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
            sat_trail.startWidth = 0.01f;
            forw_trail.startWidth = 0.01f;
        }
        else if (cam_select == 1)
        {
            pers_camera.enabled = false;
            front_camera.enabled = true;
            orb_camera.enabled = false;
            sat_trail.startWidth = 0.01f;
            forw_trail.startWidth = 0.01f;
        }
        else if (cam_select == 2)
        {
            pers_camera.enabled = false;
            front_camera.enabled = false;
            orb_camera.enabled = true;
            sat_trail.startWidth = 1f;
            forw_trail.startWidth = 1f;
        }
        else { cam_select = 0; }

        if (Input.GetKeyDown(KeyCode.RightShift)) { sat_trail.enabled = !sat_trail.enabled; }
        if (Input.GetKeyDown(KeyCode.RightControl)) { sat_trail.Clear(); }

        if (Input.GetKeyDown(KeyCode.Backspace)) { forw_trail.enabled = !forw_trail.enabled; }

        if (j < 3 && Input.GetKeyDown(KeyCode.RightArrow)) { j++; }
        if (j > 0 && Input.GetKeyDown(KeyCode.LeftArrow)) { j--; }
        if (j == 0) { Time.timeScale = 1; }
        if (j == 1) { Time.timeScale = 5; }
        if (j == 2) { Time.timeScale = 20; }
        if (j == 3) { Time.timeScale = 50; }
        ts = Time.timeScale;
    }
}
