using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 1;
    int extent;
    GameManager GM;
    [SerializeField] Renderer carRenderer;
    [SerializeField] Material matGrass, matSand, matSnow, matDark;

    void Start()
    {
        speed = Random.Range(3, 6);

        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        switch (GM.areaType)
        {
            case GameManager.AreaType.Grass:
                carRenderer.material = matGrass;
                break;
            case GameManager.AreaType.Sand:
                carRenderer.material = matSand;
                break;
            case GameManager.AreaType.Snow:
                carRenderer.material = matSnow;
                break;
            case GameManager.AreaType.Dark:
                carRenderer.material = matDark;
                break;
        }
    }

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
