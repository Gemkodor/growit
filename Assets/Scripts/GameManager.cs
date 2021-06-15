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

    public Dictionary<int, float> cropPrices = new Dictionary<int, float>();
    private CROPS currentSelectedPlant;

    public Player player;
    public bool isPlanting = false;

    private void Start()
    {
        player = GetComponent<Player>();
        cropPrices[(int)CROPS.TURNIP] = 5;
        cropPrices[(int)CROPS.ROSE] = 5;
        cropPrices[(int)CROPS.CUCUMBER] = 5;
        cropPrices[(int)CROPS.TULIP] = 5;
        cropPrices[(int)CROPS.TOMATO] = 5;
        cropPrices[(int)CROPS.MELON] = 5;
        cropPrices[(int)CROPS.EGGPLANT] = 5;
        cropPrices[(int)CROPS.LEMON] = 5;
        cropPrices[(int)CROPS.PINEAPPLE] = 5;
        cropPrices[(int)CROPS.RICE] = 5;
        cropPrices[(int)CROPS.WHEAT] = 5;
        cropPrices[(int)CROPS.GRAPES] = 5;
        cropPrices[(int)CROPS.STRAWBERRY] = 5;
        cropPrices[(int)CROPS.CASSAVA] = 5;
        cropPrices[(int)CROPS.POTATO] = 5;
        cropPrices[(int)CROPS.COFFEE] = 5;
        cropPrices[(int)CROPS.ORANGE] = 5;
        cropPrices[(int)CROPS.AVOCADO] = 5;
        cropPrices[(int)CROPS.CORN] = 5;
        cropPrices[(int)CROPS.SUNFLOWER] = 5;
    }

    public void SetPlanting(bool _isPlanting)
    {
        isPlanting = _isPlanting;
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
}
