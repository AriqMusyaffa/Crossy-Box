using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] GameObject eaglePrefab;
    [SerializeField] int spawnZPos = 7;
    [SerializeField] Player player;
    //[SerializeField] float timeOut = 0;
    [SerializeField] float timer = 7;
    [SerializeField] float timerMax = 7;
    int playerLastMaxTravel = 0;

    [SerializeField] Image eagleMeter;
    [SerializeField] GameObject stepObj, coinObj;
    GameManager GM;

    void Start()
    {
        GM = GetComponent<GameManager>();
    }

    void Update()
    {
        // Eagle Meter
        if (eagleMeter.gameObject.activeInHierarchy)
        {
            eagleMeter.fillAmount = (timer / timerMax);
        }

        // Jika Player ada kemajuan
        if (player.MaxTravel != playerLastMaxTravel)
        {
            // Reset timer
            timer = timerMax;
            playerLastMaxTravel = player.MaxTravel;
            return;
        }

        // Jika tidak maju, jalankan timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        // Jika sudah timeout
        if (player.IsJumping() == false && player.IsDie == false)
        {
            SpawnEagle();
        }
    }

    private void SpawnEagle()
    {
        player.enabled = false;
        var position = new Vector3(player.transform.position.x, 1, player.CurrentTravel + spawnZPos);
        var rotation = Quaternion.Euler(0, 180, 0);
        var eagleObject = Instantiate(eaglePrefab, position, rotation);
        var eagle = eagleObject.GetComponent<Eagle>();
        eagle.SetUpTarget(player);
        GM.SFX_Eagle();
        eagleMeter.gameObject.SetActive(false);
        stepObj.SetActive(false);
        coinObj.SetActive(false);
    }
}
