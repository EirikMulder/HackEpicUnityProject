using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YawRate : MonoBehaviour
{
    public SatelliteController satellite;
    private TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    { 
        text.text = satellite.yaw_rate.ToString();
    }
}
