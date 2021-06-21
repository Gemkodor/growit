using UnityEngine;
using UnityEngine.UI;

public class UnlockCropBtn : MonoBehaviour
{
    [SerializeField] int cropId;
    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        GetComponent<Button>().interactable = gm.player.GetMoney() >= gm.cropPrices[cropId] * gm.GetUnlockPriceFactor();
    }
}
