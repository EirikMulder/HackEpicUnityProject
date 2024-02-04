using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBackground : MonoBehaviour
{
    private Image helpme;
    // Start is called before the first frame update
    void Start()
    {
        helpme = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pausegame.pause) helpme.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        else helpme.color = Color.clear;
    }
}
