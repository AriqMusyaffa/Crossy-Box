using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public float speed = 1;
    int extent;
    public bool isRight;
    bool isTouchingPlayer = false;
    GameManager GM;
    [SerializeField] GameObject grassSandBlock, snowBlock, darkBlock;

    void Start()
    {
        speed = Random.Range(3, 5);

        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        switch (GM.areaType)
        {
            case GameManager.AreaType.Grass:
                grassSandBlock.SetActive(true);
                break;
            case GameManager.AreaType.Sand:
                grassSandBlock.SetActive(true);
                break;
            case GameManager.AreaType.Snow:
                grassSandBlock.SetActive(false);
                snowBlock.SetActive(true);
                break;
            case GameManager.AreaType.Dark:
                grassSandBlock.SetActive(false);
                darkBlock.SetActive(true);
                break;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (this.transform.position.x < -(extent + 1) || this.transform.position.x > extent + 1)
        {
            if (isTouchingPlayer)
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().guaranteeDrown = true;
            }

            Destroy(this.gameObject);
        }
    }

    public void Setup(int extent)
    {
        this.extent = extent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTouchingPlayer = false;
        }
    }
}
