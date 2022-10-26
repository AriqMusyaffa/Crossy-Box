using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestSign : MonoBehaviour
{
    GameManager GM;
    Renderer r;
    TextMeshProUGUI cr;

    void Start()
    {
        SaveLoad SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        if (SaveLoad.BestScore > 0)
        {
            transform.position = new Vector3(0, 0, SaveLoad.BestScore);
        }

        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        r = GetComponent<Renderer>();
        cr = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GM.startGame)
        {
            r.enabled = true;
            cr.enabled = true;
        }
        else
        {
            r.enabled = false;
            cr.enabled = false;
        }
    }
}
