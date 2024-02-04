using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScalar : MonoBehaviour
{

    public float relativeScale;

    public float minScale;

    public float maxScale;
    // Update is called once per frame
    void Update()
    {
        float dispDist = Vector3.Distance(Camera.main.transform.position, transform.position);
        transform.localScale = Vector3.one * Mathf.Clamp(dispDist * relativeScale, minScale, maxScale);
    }
}
