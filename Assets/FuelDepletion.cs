using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDepletion : MonoBehaviour
{
    public SatelliteController satellite;
    private Scrollbar buttonScroll;
    // Start is called before the first frame update
    void Start()
    {
        buttonScroll = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        buttonScroll.value = satellite.prop_rem / 100;
    }
}
