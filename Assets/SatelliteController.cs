using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SatelliteController : MonoBehaviour

{
    public float pitch_rate;
    public float yaw_rate;
    public float roll_rate;
    private float rot_step = 0.1f;
    private float mom_leak = -0.001f;

    public float prop_rem;
    private float prop_burn = -.75f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pitch_rate = 0f;
        yaw_rate = 0f;
        roll_rate = 0f;

        prop_rem = 100f;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prop_rem > 0)
        {
            if (Input.GetKey("s"))
            {
                pitch_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
            }
            if (Input.GetKey("w"))
            {
                pitch_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
            }

            if (Input.GetKey("d"))
            {
                yaw_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime; ;
            }
            if (Input.GetKey("a"))
            {
                yaw_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
            }

            if (Input.GetKey("e"))
            {
                roll_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
            }
            if (Input.GetKey("q"))
            {
                roll_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("Forward Thrust");
                prop_rem += 4 * prop_burn * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("Backward Thrust");
                prop_rem += 4 * prop_burn * Time.deltaTime;
            }
        }

        transform.Rotate(new Vector3(pitch_rate, roll_rate, yaw_rate)*Time.deltaTime);

        if (prop_rem < 0) {prop_rem = 0;}

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

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(transform.up*-1);
        }
    }


}
