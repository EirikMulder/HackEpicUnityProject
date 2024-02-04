using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHighlight : MonoBehaviour
{
    public string key;
    private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        if (pausegame.pause)
        {
            buttonImage.color = Color.white;
            return;
        }

        if (Input.GetKey(key))
            buttonImage.color = Color.gray;
        else
            buttonImage.color = Color.white;
    }
}
