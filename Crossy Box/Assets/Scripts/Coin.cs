using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Static akan membuat variable ini shared pada semua Coin
    public static List<Vector3> AllPositions = new List<Vector3>();
    GameManager GM;
    SaveLoad SaveLoad;
    AudioSource audio;
    bool end = false;

    private void OnEnable()
    {
        AllPositions.Add(this.transform.position);
    }

    private void OnDisable()
    {
        AllPositions.Remove(this.transform.position);
    }

    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Tree.AllPositions.Contains(transform.position) || Rock.AllPositions.Contains(transform.position))
        {
            Destroy(gameObject);
        }

        transform.Rotate(0, -0.5f, 0);

        if (end)
        {
            if (!audio.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!end)
            {
                GM.coins++;
                SaveLoad.AllCoins++;
                audio.Play();
                transform.GetComponentInChildren<Renderer>().enabled = false;
                end = true;
            }
        }
    }
}
