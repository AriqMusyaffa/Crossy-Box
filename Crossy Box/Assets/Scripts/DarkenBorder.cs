using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenBorder : MonoBehaviour
{
    void OnEnable()
    {
        GameManager GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        GameObject theParent = transform.parent.gameObject;
        Vector3 parentPosition = theParent.transform.position;

        if (theParent.CompareTag("MainBlock"))
        {
            if (theParent != transform.root.GetChild(0).gameObject)
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Renderer>().material.color *= Color.gray;
                }
            }
        }
        else
        {
            if (parentPosition.x == (GM.extent + 1) || parentPosition.x == -(GM.extent + 1))
            {
                foreach (Transform child in transform)
                {
                    child.GetComponent<Renderer>().material.color *= Color.gray;
                }
            }
        }
    }
}
