using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BooleanCharge : MonoBehaviour
{
    public SatelliteController satellite;
    private Image check;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (satellite.charge)
            check.color = Color.green;
        else
            check.color = Color.red;

            
    }
}
