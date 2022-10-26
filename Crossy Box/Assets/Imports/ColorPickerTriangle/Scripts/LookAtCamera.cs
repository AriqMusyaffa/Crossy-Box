using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform camera;

    void Start()
    {
        camera = Camera.main.transform;
        transform.LookAt(camera);

        if (GetComponent<ColorPickerTriangle>() != null)
        {
            transform.rotation = Quaternion.Euler(-30.482f, 150f, 0);
        }
    }

    void Update()
    {
        
    }
}
