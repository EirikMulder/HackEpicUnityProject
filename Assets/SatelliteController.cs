using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteController : MonoBehaviour

{
    private float pitch_rate = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pitch_rate -= 0.5f;            
        }
        Camera.main.transform.Rotate(new Vector3(pitch_rate, 0, 0));
    }


}
