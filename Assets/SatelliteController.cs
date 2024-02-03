using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour

{
    private float pitch_rate = 0f;
    private float yaw_rate = 0f;
    private float rot_step = 0.002f;
    private float mom_leak = -0.0001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pitch_rate -= rot_step;            
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pitch_rate += rot_step;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            yaw_rate -= rot_step;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            yaw_rate += rot_step;
        }

        transform.Rotate(new Vector3(pitch_rate, yaw_rate, 0));

        if (pitch_rate != 0)
        {
            pitch_rate = (Mathf.Abs(pitch_rate) + mom_leak) * (Mathf.Abs(pitch_rate) / pitch_rate);
        }
    }


}
