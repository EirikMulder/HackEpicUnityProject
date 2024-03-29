//using Palmmedia.ReportGenerator.Core.Parser.Analysis;
//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
using System;
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

    public bool prop_out = false;
    public bool batt_out = false;

    private float point_acc;

    public bool charge;

    private float dist_effect;

    // Start is called before the first frame update
    void Start()
    {
        pitch_rate = 0f;
        yaw_rate = 0f;
        roll_rate = 0f;

        prop_rem = 100f;
        batt_charge = 100f;

        rb = GetComponent<Rigidbody>();

        charge = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pausegame.pause) return;

        if (!prop_out && !batt_out)
        {
            if (Input.GetKey("s"))
            {
                pitch_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.75f * batt_decay * Time.deltaTime;
            }
            if (Input.GetKey("w"))
            {
                pitch_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.75f * batt_decay * Time.deltaTime;
            }

            if (Input.GetKey("d"))
            {
                yaw_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.75f * batt_decay * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                yaw_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.75f * batt_decay * Time.deltaTime;
            }

            if (Input.GetKey("e"))
            {
                roll_rate -= rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.75f * batt_decay * Time.deltaTime;
            }
            if (Input.GetKey("q"))
            {
                roll_rate += rot_step;
                prop_rem += prop_burn * Time.deltaTime;
                batt_charge += 0.75f * batt_decay * Time.deltaTime;
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
        }

        batt_charge += 0.5f * batt_decay * Time.deltaTime;

        transform.Rotate(new Vector3(pitch_rate, roll_rate, yaw_rate) * Time.deltaTime);

        if (prop_rem < 0)
        {
            prop_rem = 0;
            prop_out = true;
        }
        if (batt_charge <= 0)
        {
            batt_charge = 0;
            batt_out = true;
        }
        else batt_out = false;
        if (batt_charge > 100) { batt_charge = 100; }

        if (pitch_rate != 0 && !batt_out)
        {
            pitch_rate = (Mathf.Abs(pitch_rate) + mom_leak) * (Mathf.Abs(pitch_rate) / pitch_rate);
            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }
        if (yaw_rate != 0 && !batt_out)
        {
            yaw_rate = (Mathf.Abs(yaw_rate) + mom_leak) * (Mathf.Abs(yaw_rate) / yaw_rate);
            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }
        if (roll_rate != 0 && !batt_out)
        {
            roll_rate = (Mathf.Abs(roll_rate) + mom_leak) * (Mathf.Abs(roll_rate) / roll_rate);
            batt_charge += 0.25f * batt_decay * Time.deltaTime;
        }

        if (Mathf.Abs(pitch_rate) < Mathf.Abs(mom_leak)) { pitch_rate = 0; }
        if (Mathf.Abs(roll_rate) < Mathf.Abs(mom_leak)) { roll_rate = 0; }
        if (Mathf.Abs(yaw_rate) < Mathf.Abs(mom_leak)) { yaw_rate = 0; }

        if (prop_out) { Debug.Log("Propellent Out!"); }
        if (batt_out) { Debug.Log("Battery Dead!"); }

        point_acc = Vector3.Dot(transform.up, Vector3.forward);
        if (point_acc > 0)
        {
            if (transform.position[2] > 0 && Mathf.Sqrt(Mathf.Pow(transform.position[0], 2) + Mathf.Pow(transform.position[1], 2)) < 2.5)
            {
                charge = false;
            }
            else
            {
                batt_charge -= 3 * point_acc * batt_decay * Time.deltaTime;
                charge = true;
            }
        }
        else { charge = false; }
    }

    private void FixedUpdate()
    {
        if (pausegame.pause) return;

        if (prop_out || batt_out) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0.15f * transform.up);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-0.15f * transform.up);
        }
    }
}