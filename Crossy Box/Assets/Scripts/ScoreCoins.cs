using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCoins : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] Player player;
    SaveLoad SaveLoad;

    [Header("Menu")]
    [SerializeField] GameObject menu_steps;
    [SerializeField] Image ms_first;
    [SerializeField] Image ms_second;
    [SerializeField] Image ms_third;
    [SerializeField] GameObject menu_coins;
    [SerializeField] Image mc_first;
    [SerializeField] Image mc_second;
    [SerializeField] Image mc_third;

    [Header("Gameplay")]
    [SerializeField] GameObject play_steps;
    [SerializeField] Image ps_first;
    [SerializeField] Image ps_second;
    [SerializeField] Image ps_third;
    [SerializeField] GameObject play_coins_1;
    [SerializeField] Image pc1_first;
    [SerializeField] GameObject play_coins_2;
    [SerializeField] Image pc2_first;
    [SerializeField] Image pc2_second;
    [SerializeField] GameObject play_coins_3;
    [SerializeField] Image pc3_first;
    [SerializeField] Image pc3_second;
    [SerializeField] Image pc3_third;

    [Header("Game Over")]
    [SerializeField] GameObject end_steps1;
    [SerializeField] Image es1_first;
    [SerializeField] GameObject end_steps2;
    [SerializeField] Image es2_first;
    [SerializeField] Image es2_second;
    [SerializeField] GameObject end_steps3;
    [SerializeField] Image es3_first;
    [SerializeField] Image es3_second;
    [SerializeField] Image es3_third;
    [SerializeField] GameObject end_coins1;
    [SerializeField] Image ec1_first;
    [SerializeField] GameObject end_coins2;
    [SerializeField] Image ec2_first;
    [SerializeField] Image ec2_second;
    [SerializeField] GameObject end_coins3;
    [SerializeField] Image ec3_first;
    [SerializeField] Image ec3_second;
    [SerializeField] Image ec3_third;

    [Header("Number Sprites")]
    [SerializeField] Sprite[] numbers;
    [SerializeField] Sprite[] numbersMini;

    [Header("Variables")]
    [SerializeField] int highscoreTens;
    [SerializeField] int highscoreHundreds;
    [SerializeField] int totalcoinsTens;
    [SerializeField] int totalcoinsHundreds;
    [SerializeField] int stepTens = 0;
    [SerializeField] int stepHundreds = 0;
    [SerializeField] int coinTens = 0;
    [SerializeField] int coinHundreds = 0;

    void Start()
    {
        SaveLoad = GameObject.FindWithTag("SaveLoad").GetComponent<SaveLoad>();

        // Menu Setup
        string highScore1 = SaveLoad.BestScore.ToString();
        string totalCoins1 = SaveLoad.AllCoins.ToString();

        // Menu Highscore
        if (SaveLoad.BestScore < 10)
        {
            char hsChar1 = highScore1[0];
            int hs1 = hsChar1 - '0';

            ms_first.gameObject.SetActive(true);
            ms_first.sprite = numbersMini[hs1];
        }
        else if (SaveLoad.BestScore < 100)
        {
            char hsChar1 = highScore1[0];
            int hs1 = hsChar1 - '0';
            char hsChar2 = highScore1[1];
            int hs2 = hsChar2 - '0';

            ms_first.gameObject.SetActive(true);
            ms_second.gameObject.SetActive(true);
            ms_first.sprite = numbersMini[hs1];
            ms_second.sprite = numbersMini[hs2];
        }
        else
        {
            char hsChar1 = highScore1[0];
            int hs1 = hsChar1 - '0';
            char hsChar2 = highScore1[1];
            int hs2 = hsChar2 - '0';
            char hsChar3 = highScore1[2];
            int hs3 = hsChar3 - '0';

            ms_first.gameObject.SetActive(true);
            ms_second.gameObject.SetActive(true);
            ms_third.gameObject.SetActive(true);
            ms_first.sprite = numbersMini[hs1];
            ms_second.sprite = numbersMini[hs2];
            ms_third.sprite = numbersMini[hs3];
        }

        // Menu Total Coins
        if (SaveLoad.AllCoins < 10)
        {
            char tcChar1 = totalCoins1[0];
            int tc1 = tcChar1 - '0';

            mc_first.gameObject.SetActive(true);
            mc_first.sprite = numbersMini[tc1];
        }
        else if (SaveLoad.AllCoins < 100)
        {
            char tcChar1 = totalCoins1[0];
            int tc1 = tcChar1 - '0';
            char tcChar2 = totalCoins1[1];
            int tc2 = tcChar2 - '0';

            mc_first.gameObject.SetActive(true);
            mc_second.gameObject.SetActive(true);
            mc_first.sprite = numbersMini[tc1];
            mc_second.sprite = numbersMini[tc2];
        }
        else
        {
            char tcChar1 = totalCoins1[0];
            int tc1 = tcChar1 - '0';
            char tcChar2 = totalCoins1[1];
            int tc2 = tcChar2 - '0';
            char tcChar3 = totalCoins1[2];
            int tc3 = tcChar3 - '0';

            mc_first.gameObject.SetActive(true);
            mc_second.gameObject.SetActive(true);
            mc_third.gameObject.SetActive(true);
            mc_first.sprite = numbersMini[tc1];
            mc_second.sprite = numbersMini[tc2];
            mc_third.sprite = numbersMini[tc3];
        }
    }

    void Update()
    {
        if (GM.startGame)
        {
            // Play Steps
            switch (stepTens)
            {
                case 0:
                    if (player.MaxTravel > 9)
                    {
                        stepTens = 1;
                    }
                    break;
                case 1:
                    if (player.MaxTravel > 19)
                    {
                        stepTens = 2;
                    }
                    break;
                case 2:
                    if (player.MaxTravel > 29)
                    {
                        stepTens = 3;
                    }
                    break;
                case 3:
                    if (player.MaxTravel > 39)
                    {
                        stepTens = 4;
                    }
                    break;
                case 4:
                    if (player.MaxTravel > 49)
                    {
                        stepTens = 5;
                    }
                    break;
                case 5:
                    if (player.MaxTravel > 59)
                    {
                        stepTens = 6;
                    }
                    break;
                case 6:
                    if (player.MaxTravel > 69)
                    {
                        stepTens = 7;
                    }
                    break;
                case 7:
                    if (player.MaxTravel > 79)
                    {
                        stepTens = 8;
                    }
                    break;
                case 8:
                    if (player.MaxTravel > 89)
                    {
                        stepTens = 9;
                    }
                    break;
                case 9:
                    if (player.MaxTravel > 99)
                    {
                        if (stepHundreds == 0)
                        {
                            stepHundreds = 1;
                        }
                        stepTens = 0;
                    }
                    break;
            }

            switch (stepHundreds)
            {
                case 1:
                    if (player.MaxTravel > 199)
                    {
                        stepHundreds = 2;
                    }
                    break;
                case 2:
                    if (player.MaxTravel > 299)
                    {
                        stepHundreds = 3;
                    }
                    break;
                case 3:
                    if (player.MaxTravel > 399)
                    {
                        stepHundreds = 4;
                    }
                    break;
                case 4:
                    if (player.MaxTravel > 499)
                    {
                        stepHundreds = 5;
                    }
                    break;
                case 5:
                    if (player.MaxTravel > 599)
                    {
                        stepHundreds = 6;
                    }
                    break;
                case 6:
                    if (player.MaxTravel > 699)
                    {
                        stepHundreds = 7;
                    }
                    break;
                case 7:
                    if (player.MaxTravel > 799)
                    {
                        stepHundreds = 8;
                    }
                    break;
                case 8:
                    if (player.MaxTravel > 899)
                    {
                        stepHundreds = 9;
                    }
                    break;
            }

            if (stepTens == 0 && stepHundreds == 0)
            {
                ps_first.gameObject.SetActive(true);
                ps_first.sprite = numbers[player.MaxTravel];
            }
            else if (stepTens > 0 && stepHundreds == 0)
            {
                ps_second.gameObject.SetActive(true);
                ps_first.sprite = numbers[stepTens];
                ps_second.sprite = numbers[player.MaxTravel - (stepTens * 10)];
            }
            else if (stepTens > 0 && stepHundreds > 0 && stepHundreds < 1000)
            {
                ps_third.gameObject.SetActive(true);
                ps_first.sprite = numbers[stepHundreds];
                ps_second.sprite = numbers[stepTens];
                ps_third.sprite = numbers[player.MaxTravel - (stepHundreds * 100)];
            }

            // Play Coins
            switch (coinTens)
            {
                case 0:
                    if (GM.coins > 9)
                    {
                        coinTens = 1;
                    }
                    break;
                case 1:
                    if (GM.coins > 19)
                    {
                        coinTens = 2;
                    }
                    break;
                case 2:
                    if (GM.coins > 29)
                    {
                        coinTens = 3;
                    }
                    break;
                case 3:
                    if (GM.coins > 39)
                    {
                        coinTens = 4;
                    }
                    break;
                case 4:
                    if (GM.coins > 49)
                    {
                        coinTens = 5;
                    }
                    break;
                case 5:
                    if (GM.coins > 59)
                    {
                        coinTens = 6;
                    }
                    break;
                case 6:
                    if (GM.coins > 69)
                    {
                        coinTens = 7;
                    }
                    break;
                case 7:
                    if (GM.coins > 79)
                    {
                        coinTens = 8;
                    }
                    break;
                case 8:
                    if (GM.coins > 89)
                    {
                        coinTens = 9;
                    }
                    break;
                case 9:
                    if (GM.coins > 99)
                    {
                        if (coinHundreds == 0)
                        {
                            coinHundreds = 1;
                        }
                        coinTens = 0;
                    }
                    break;
            }

            switch (coinHundreds)
            {
                case 1:
                    if (GM.coins > 199)
                    {
                        coinHundreds = 2;
                    }
                    break;
                case 2:
                    if (GM.coins > 299)
                    {
                        coinHundreds = 3;
                    }
                    break;
                case 3:
                    if (GM.coins > 399)
                    {
                        coinHundreds = 4;
                    }
                    break;
                case 4:
                    if (GM.coins > 499)
                    {
                        coinHundreds = 5;
                    }
                    break;
                case 5:
                    if (GM.coins > 599)
                    {
                        coinHundreds = 6;
                    }
                    break;
                case 6:
                    if (GM.coins > 699)
                    {
                        coinHundreds = 7;
                    }
                    break;
                case 7:
                    if (GM.coins > 799)
                    {
                        coinHundreds = 8;
                    }
                    break;
                case 8:
                    if (GM.coins > 899)
                    {
                        coinHundreds = 9;
                    }
                    break;
            }

            if (coinTens == 0 && coinHundreds == 0)
            {
                play_coins_1.gameObject.SetActive(true);
                pc1_first.sprite = numbers[GM.coins];
            }
            else if (coinTens > 0 && coinHundreds == 0)
            {
                play_coins_1.gameObject.SetActive(false);
                play_coins_2.gameObject.SetActive(true);
                pc2_first.sprite = numbers[coinTens];
                pc2_second.sprite = numbers[GM.coins - (coinTens * 10)];
            }
            else if (coinTens > 0 && coinHundreds > 0 && coinHundreds < 1000)
            {
                play_coins_1.gameObject.SetActive(false);
                play_coins_2.gameObject.SetActive(false);
                play_coins_3.gameObject.SetActive(true);
                pc3_first.sprite = numbers[coinHundreds];
                pc3_second.sprite = numbers[coinTens];
                pc3_third.sprite = numbers[GM.coins - (coinHundreds * 100)];
            }

            // Game Over Steps
            if (stepTens == 0 && stepHundreds == 0)
            {
                end_steps1.SetActive(true);
                es1_first.sprite = numbers[player.MaxTravel];
            }
            else if (stepTens > 0 && stepHundreds == 0)
            {
                end_steps1.SetActive(false);
                end_steps2.SetActive(true);
                es2_first.sprite = numbers[stepTens];
                es2_second.sprite = numbers[player.MaxTravel - (stepTens * 10)];
            }
            else if (stepTens > 0 && stepHundreds > 0)
            {
                end_steps1.SetActive(false);
                end_steps2.SetActive(false);
                end_steps3.SetActive(true);
                es3_first.sprite = numbers[stepHundreds];
                es3_second.sprite = numbers[stepTens];
                es3_third.sprite = numbers[player.MaxTravel - (stepHundreds * 100)];
            }

            // Game Over Coins
            if (coinTens == 0 && coinHundreds == 0)
            {
                end_coins1.SetActive(true);
                ec1_first.sprite = numbers[GM.coins];
            }
            else if (coinTens > 0 && coinHundreds == 0)
            {
                end_coins1.SetActive(false);
                end_coins2.SetActive(true);
                ec2_first.sprite = numbers[coinTens];
                ec2_second.sprite = numbers[GM.coins - (coinTens * 10)];
            }
            else if (coinTens > 0 && coinHundreds > 0)
            {
                end_coins1.SetActive(false);
                end_coins2.SetActive(false);
                end_coins3.SetActive(true);
                ec3_first.sprite = numbers[coinHundreds];
                ec3_second.sprite = numbers[coinTens];
                ec3_third.sprite = numbers[GM.coins - (coinHundreds * 100)];
            }
        }
    }
}
