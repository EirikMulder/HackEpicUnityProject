using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class pausescreen : MonoBehaviour
{
    private TextMeshProUGUI pause_image;

    // Start is called before the first frame update
    void Start()
    {
        pause_image = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pausegame.pause) pause_image.color = Color.white; 
        else pause_image.color = Color.clear;
    }
}
