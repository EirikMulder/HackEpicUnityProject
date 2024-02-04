using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class batt_warning : MonoBehaviour
{
    public SatelliteController sat_batt;
    private TextMeshProUGUI batt_dead;
    // Start is called before the first frame update
    void Start()
    {
        batt_dead = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pausegame.pause) return;
        if (sat_batt.batt_out) batt_dead.color = Color.red; else batt_dead.color = Color.clear;
    }
}
