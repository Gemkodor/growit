using UnityEngine;
using UnityEngine.UI;

public class ShopRow : MonoBehaviour
{
    [SerializeField] int cropId;
    [SerializeField] Text playerCropQty;
    GameManager gm;
    Player player;
    Button[] buttons;
    float cropPrice = 0;

    void Start()
    {
        player = FindObjectOfType<Player>();
        buttons = GetComponentsInChildren<Button>();
        gm = FindObjectOfType<GameManager>();
        cropPrice = gm.cropPrices[cropId];
    }

    void Update()
    {
        SetButtonsActivity();    
    }

    private void SetButtonsActivity()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            switch (buttons[i].gameObject.name)
            {
                case "BuySingleCrop":
                    buttons[i].interactable = player.GetMoney() >= cropPrice;
                    break;
                case "BuyTenCrops":
                    buttons[i].interactable = player.GetMoney() >= cropPrice * 10;
                    break;
                case "SellSingleCrop":
                    buttons[i].interactable = int.Parse(playerCropQty.text) >= 1;
                    break;
                case "SellTenCrops":
                    buttons[i].interactable = int.Parse(playerCropQty.text) >= 10;
                    break;
            }
        }
    }
}
