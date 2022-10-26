using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPreview : MonoBehaviour
{
    SaveLoad SaveLoad;
    [SerializeField] GameManager GM;
    [SerializeField] Material defaultColor;
    [SerializeField] Recolor[] recolor;
    [SerializeField] RainbowColor[] rainbowColor;
    [SerializeField] GameObject colorPickerObject;
    ColorPickerTriangle cpt;
    [SerializeField] GameObject grass, autumn, snow, sand, dark;
    bool redarken = true;
    Color customColor;
    bool firstColorPick = false;
    [SerializeField] Camera camera;
    Color waterColor = new Color(28f / 255f, 163f / 255f, 236f / 255f);
    Color sandColor = new Color(210f / 255f, 175f / 255f, 80f / 255f);
    Color lavaColor = new Color(200f / 255f, 0f / 255f, 50f / 255f);
    Color darken = new Color(225f / 255f, 225f / 255f, 225f / 255f);

    void Start()
    {
        SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();

        switch (SaveLoad.AreaType)
        {
            case "Grass":
                AreaGrass();
                break;
            case "Autumn":
                AreaAutumn();
                break;
            case "Snow":
                AreaSnow();
                break;
            case "Sand":
                AreaSand();
                break;
            case "Dark":
                AreaDark();
                break;
        }

        cpt = colorPickerObject.GetComponent<ColorPickerTriangle>();

        //waterColor *= darken;
        //sandColor *= darken;
        //lavaColor *= darken;
    }

    public void ColorDefault()
    {
        GM.SFX_Click();

        foreach (RainbowColor r in rainbowColor)
        {
            r.isRainbow = false;
        }
        foreach (Recolor r in recolor)
        {
            r.isPaint = false;
            r.GetComponent<Renderer>().material = defaultColor;
            r.Redarken();
        }

        colorPickerObject.SetActive(false);
        SaveLoad.playerIsColor = false;
        SaveLoad.playerIsRainbow = false;
    }

    public void ColorCustom()
    {
        GM.SFX_Click();

        foreach (RainbowColor r in rainbowColor)
        {
            r.isRainbow = false;
        }
        foreach (Recolor r in recolor)
        {
            r.isPaint = true;
        }

        colorPickerObject.SetActive(true);

        if (!firstColorPick)
        {
            foreach (Transform child in colorPickerObject.transform)
            {
                child.GetComponent<Renderer>().enabled = true;
            }

            firstColorPick = true;
        }

        SaveLoad.playerIsRainbow = false;
        SaveLoad.playerIsColor = true;
    }

    public void ColorRainbow()
    {
        GM.SFX_Click();

        foreach (Recolor r in recolor)
        {
            r.isPaint = false;
        }
        foreach (RainbowColor r in rainbowColor)
        {
            r.isRainbow = true;
        }

        colorPickerObject.SetActive(false);
        SaveLoad.playerIsColor = false;
        SaveLoad.playerIsRainbow = true;
    }

    public void AreaGrass()
    {
        GM.SFX_Click();

        SaveLoad.AreaType = "Grass";

        grass.SetActive(true);
        autumn.SetActive(false);
        snow.SetActive(false);
        sand.SetActive(false);
        dark.SetActive(false);

        camera.backgroundColor = waterColor;
    }

    public void AreaAutumn()
    {
        GM.SFX_Click();

        SaveLoad.AreaType = "Autumn";

        grass.SetActive(false);
        autumn.SetActive(true);
        snow.SetActive(false);
        sand.SetActive(false);
        dark.SetActive(false);

        camera.backgroundColor = waterColor;
    }

    public void AreaSnow()
    {
        GM.SFX_Click();

        SaveLoad.AreaType = "Snow";

        grass.SetActive(false);
        autumn.SetActive(false);
        snow.SetActive(true);
        sand.SetActive(false);
        dark.SetActive(false);

        camera.backgroundColor = waterColor;
    }

    public void AreaSand()
    {
        GM.SFX_Click();

        SaveLoad.AreaType = "Sand";

        grass.SetActive(false);
        autumn.SetActive(false);
        snow.SetActive(false);
        sand.SetActive(true);
        dark.SetActive(false);

        camera.backgroundColor = sandColor;
    }

    public void AreaDark()
    {
        GM.SFX_Click();

        SaveLoad.AreaType = "Dark";

        grass.SetActive(false);
        autumn.SetActive(false);
        snow.SetActive(false);
        sand.SetActive(false);
        dark.SetActive(true);

        camera.backgroundColor = lavaColor;
    }

    void Update()
    {
        if (SaveLoad.playerIsColor)
        {
            SaveLoad.playerColor = cpt.TheColor;
        }
    }
}
