using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour

{
    private float pitch_rate = 0f;
    private float yaw_rate = 0f;
    private float roll_rate = 0f;
    private float rot_step = 0.1f;
    private float mom_leak = -0.001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("s"))
        {
            pitch_rate += rot_step;            
        }
        if (Input.GetKey("w"))
        {
            pitch_rate -= rot_step;
        }

        if (Input.GetKey("d"))
        {
            yaw_rate -= rot_step;
        }
        if (Input.GetKey("a"))
        {
            yaw_rate += rot_step;
        }

        if (Input.GetKey("e"))
        {
            roll_rate -= rot_step;
        }
        if (Input.GetKey("q"))
        {
            roll_rate += rot_step;
        }

        transform.Rotate(new Vector3(pitch_rate, roll_rate, yaw_rate)*Time.deltaTime);

        if (pitch_rate != 0)
        {
            pitch_rate = (Mathf.Abs(pitch_rate) + mom_leak) * (Mathf.Abs(pitch_rate) / pitch_rate);
        }
        if (yaw_rate != 0)
        {
            yaw_rate = (Mathf.Abs(yaw_rate) + mom_leak) * (Mathf.Abs(yaw_rate) / yaw_rate);
        }
        if (roll_rate != 0)
        {
            roll_rate = (Mathf.Abs(roll_rate) + mom_leak) * (Mathf.Abs(roll_rate) / roll_rate);
        }
    }


}
