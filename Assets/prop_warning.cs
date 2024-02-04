using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class prop_warning : MonoBehaviour
{
    public SatelliteController sat_prop;
    private TextMeshProUGUI prop_dead;
    // Start is called before the first frame update
    void Start()
    {
        prop_dead = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pausegame.pause) return;
        if (sat_prop.prop_out) prop_dead.color = Color.red; else prop_dead.color = Color.clear;
    }
}
