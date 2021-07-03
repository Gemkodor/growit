using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum CROPS
    {
        TURNIP = 0,
        ROSE = 1,
        CUCUMBER = 2,
        TULIP = 3,
        TOMATO = 4,
        MELON = 5,
        EGGPLANT = 6,
        LEMON = 7,
        PINEAPPLE = 8,
        RICE = 9,
        WHEAT = 10,
        GRAPES = 11,
        STRAWBERRY = 12,
        CASSAVA = 13,
        POTATO = 14,
        COFFEE = 15,
        ORANGE = 16,
        AVOCADO = 17,
        CORN = 18,
        SUNFLOWER = 19
    }

    [SerializeField] GameObject[] cropsPrefabs;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject loadGamePanel;
    [SerializeField] Text loadGameStr;

    public Player player;
    public bool isPlanting = false;
    public Dictionary<int, float> cropPrices = new Dictionary<int, float>();
    public Dictionary<CROPS, bool> cropsAvailable = new Dictionary<CROPS, bool>();
    private CROPS currentSelectedPlant;
    private int priceFactorForUnlock = 3;

    private void Awake()
    {
        player = GetComponent<Player>();
        SetCropPrices();
        SetAvailableCrops();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("playerSaved", 1);
        PlayerPrefs.SetFloat("playerMoney", player.GetMoney());

        foreach (CROPS crop in Enum.GetValues(typeof(CROPS)))
        {
            string text = "locked";
            if (cropsAvailable[crop])
            {
                text = "unlock";
            }
            PlayerPrefs.SetString(crop.ToString(), text);

            PlayerPrefs.SetInt(crop.ToString() + "Qty", player.crops[crop.ToString()]);
        }
    }

    public void OpenLoadGamePanel()
    {
        loadGamePanel.SetActive(true);
    }

    public void CloseLoadGamePanel()
    {
        loadGamePanel.SetActive(false);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("playerSaved") == 1)
        {
            Debug.Log("Load game"); 
            player.SetMoney(PlayerPrefs.GetFloat("playerMoney"));

            foreach (CROPS crop in Enum.GetValues(typeof(CROPS)))
            {
                bool unlocked = PlayerPrefs.GetString(crop.ToString()) == "unlock";
                cropsAvailable[crop] = unlocked;

                int qty = PlayerPrefs.GetInt(crop.ToString() + "Qty", -1);
                if (qty > -1)
                {
                    player.crops[crop.ToString()] = qty;
                }
            }
        } 
        else
        {
            Debug.Log("No loading");
        }

        loadGamePanel.SetActive(false);
    }

    private void SetCropPrices()
    {
        cropPrices[(int)CROPS.TURNIP] = 13;
        cropPrices[(int)CROPS.ROSE] = 21;
        cropPrices[(int)CROPS.CUCUMBER] = 34;
        cropPrices[(int)CROPS.TULIP] = 55;
        cropPrices[(int)CROPS.TOMATO] = 89;
        cropPrices[(int)CROPS.MELON] = 144;
        cropPrices[(int)CROPS.EGGPLANT] = 233;
        cropPrices[(int)CROPS.LEMON] = 377;
        cropPrices[(int)CROPS.PINEAPPLE] = 610;
        cropPrices[(int)CROPS.RICE] = 987;
        cropPrices[(int)CROPS.WHEAT] = 1597;
        cropPrices[(int)CROPS.GRAPES] = 2584;
        cropPrices[(int)CROPS.STRAWBERRY] = 4181;
        cropPrices[(int)CROPS.CASSAVA] = 6765;
        cropPrices[(int)CROPS.POTATO] = 10946;
        cropPrices[(int)CROPS.COFFEE] = 17711;
        cropPrices[(int)CROPS.ORANGE] = 28657;
        cropPrices[(int)CROPS.AVOCADO] = 46368;
        cropPrices[(int)CROPS.CORN] = 75025;
        cropPrices[(int)CROPS.SUNFLOWER] = 121393;
    }

    private void SetAvailableCrops()
    {
        foreach (CROPS crop in Enum.GetValues(typeof(CROPS)))
        {
            cropsAvailable[crop] = false;
        }
        cropsAvailable[CROPS.TURNIP] = true;
    }

    public void UnlockCrop(int cropId)
    {
        if (player.GetMoney() >= cropPrices[cropId] * priceFactorForUnlock)
        {
            cropsAvailable[(CROPS)cropId] = true;
            player.SetMoney(player.GetMoney() - cropPrices[cropId]);
        }
    }

    public void SetPlanting(bool _isPlanting)
    {
        isPlanting = _isPlanting;
    }

    public void SwitchMode()
    {
        isPlanting = !isPlanting;
    }

    public void SelectCrop(int cropToSelect)
    {
        currentSelectedPlant = (CROPS) cropToSelect;
        isPlanting = true;
    }

    public GameObject GetSelectedCrop()
    {
        return cropsPrefabs[(int) currentSelectedPlant];
    }

    public CROPS GetSelectedCropType()
    {
        return currentSelectedPlant;
    }

    private void BuyCrop(int cropId, int qty)
    {
        float playerMoney = player.GetMoney();
        float cropPrice = cropPrices[cropId];

        if (player.GetMoney() >= cropPrice * qty)
        {
            player.AddCrop((CROPS)cropId, qty);
            player.SetMoney(playerMoney - (cropPrice * qty));
        }
    }

    public void BuySingleCrop(int cropId)
    {
        BuyCrop(cropId, 1);
    }

    public void BuyTenCrops(int cropId)
    {
        BuyCrop(cropId, 10);
    }

    public void BuyHundredCrops(int cropId)
    {
        BuyCrop(cropId, 100);
    }

    private void SellCrop(int cropId, int qty)
    {
        float playerMoney = player.GetMoney();
        float playerCropQty = player.crops[Enum.GetName(typeof(CROPS), cropId)];
        float cropPrice = cropPrices[cropId];

        if (playerCropQty >= qty)
        {
            player.RemoveCrop((CROPS)cropId, qty);
            player.SetMoney(playerMoney + (cropPrice * qty));
        }
    }
    
    public void SellSingleCrop(int cropId)
    {
        SellCrop(cropId, 1);
    }

    public void SellTenCrops(int cropId)
    {
        SellCrop(cropId, 10);
    }

    public void SellAllCrops(int cropId)
    {
        SellCrop(cropId, player.crops[Enum.GetName(typeof(CROPS), cropId)]);
    }

    public int GetUnlockPriceFactor()
    {
        return priceFactorForUnlock;
    }
}
