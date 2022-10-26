using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenColor : MonoBehaviour
{
    Color darken = new Color(225f / 255f, 225f / 255f, 225f / 255f);

    void Start()
    {
        GetComponent<Renderer>().material.color *= darken;
    }

    public void Redarken()
    {
        GetComponent<Renderer>().material.color *= darken;
    }
}
