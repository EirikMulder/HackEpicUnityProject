//using Palmmedia.ReportGenerator.Core.Parser.Analysis;
//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
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

    public float batt_charge;
    private float batt_decay = -1;

    private Rigidbody rb;

    private bool prop_out = false;
    private bool batt_out = false;

    private float point_acc;

    // Start is called before the first frame update
    void Start()
    {
        pitch_rate = 0f;
        yaw_rate = 0f;
        roll_rate = 0f;

        prop_rem = 100f;
        batt_charge = 100f;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prop_rem > 0 && batt_charge > 0)
        {
            if (Input.GetKey("s"))
            {
                pitch_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.5f * batt_decay * Time.deltaTime;
            }
            if (Input.GetKey("w"))
            {
                pitch_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.5f * batt_decay * Time.deltaTime;
            }

            if (Input.GetKey("d"))
            {
                yaw_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.5f * batt_decay * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                yaw_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.5f * batt_decay * Time.deltaTime;
            }

            if (Input.GetKey("e"))
            {
                roll_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.5f * batt_decay * Time.deltaTime;
            }
            if (Input.GetKey("q"))
            {
                roll_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.5f * batt_decay * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                prop_rem += 4 * prop_burn * Time.deltaTime;
                batt_charge += batt_decay * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                prop_rem += 4 * prop_burn * Time.deltaTime;
                batt_charge += batt_decay * Time.deltaTime;
            }

            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }

        transform.Rotate(new Vector3(pitch_rate, roll_rate, yaw_rate)*Time.deltaTime);

        if (prop_rem < 0)
        {
            prop_rem = 100;
            prop_out = true;
        }
        if (batt_charge < 0)
        {
            batt_charge = 100;
            batt_out = true;
        }
        if (batt_charge > 100) { batt_charge = 100; }

        if (pitch_rate != 0)
        {
            pitch_rate = (Mathf.Abs(pitch_rate) + mom_leak) * (Mathf.Abs(pitch_rate) / pitch_rate);
            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }
        if (yaw_rate != 0)
        {
            yaw_rate = (Mathf.Abs(yaw_rate) + mom_leak) * (Mathf.Abs(yaw_rate) / yaw_rate);
            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }
        if (roll_rate != 0)
        {
            roll_rate = (Mathf.Abs(roll_rate) + mom_leak) * (Mathf.Abs(roll_rate) / roll_rate);
            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }

        if (Mathf.Abs(pitch_rate) < Mathf.Abs(mom_leak)) { pitch_rate = 0; }
        if (Mathf.Abs(roll_rate) < Mathf.Abs(mom_leak)) { roll_rate = 0; }
        if (Mathf.Abs(yaw_rate) < Mathf.Abs(mom_leak)) { yaw_rate = 0; }

        if (prop_out) { Debug.Log("Propellent Out!"); }
        if (batt_out) { Debug.Log("Battery Dead!"); }

        point_acc = Vector3.Dot(transform.up,Vector3.forward);
        if (point_acc > 0) { batt_charge -= point_acc * batt_decay * Time.deltaTime; }
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
