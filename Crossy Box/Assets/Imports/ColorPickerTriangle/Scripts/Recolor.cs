using UnityEngine;
using System.Collections;

public class Recolor : MonoBehaviour {

    public GameObject ColorPickedPrefab;
    private ColorPickerTriangle CP;
    public bool isPaint = false;
    private GameObject go;
    Renderer renderer;
    DarkenColor darkenColor;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        CP = ColorPickedPrefab.GetComponent<ColorPickerTriangle>();

        SaveLoad SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();
        CP.SetNewColor(SaveLoad.playerColor);

        if (GetComponent<DarkenColor>() != null)
        {
            darkenColor = GetComponent<DarkenColor>();
        }
    }

    void Update()
    {
        if (isPaint)
        {
            renderer.material.color = CP.TheColor;

            Redarken();
        }
    }

    public void Redarken()
    {
        if (darkenColor != null)
        {
            darkenColor.Redarken();
        }
    }
}
