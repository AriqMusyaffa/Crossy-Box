using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] float speed = 20;
    int extent;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (this.transform.position.x < -(extent + 1) || this.transform.position.x > extent + 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(int extent)
    {
        this.extent = extent;
    }
}
