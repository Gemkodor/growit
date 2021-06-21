using UnityEngine;
using UnityEngine.UI;

public class CropTreeItem : MonoBehaviour
{
    [SerializeField] int cropId;
    Text priceLbl;
    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        float price = gm.cropPrices[cropId] * gm.GetUnlockPriceFactor();
        priceLbl = transform.GetChild(1).GetComponent<Text>();
        priceLbl.text = price.ToString() + " €";
    }

    void Update()
    {
        if (gm.cropsAvailable[(GameManager.CROPS) cropId])
        {
            GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.8f, 0.5f);
            transform.GetChild(3).gameObject.SetActive(false); // Hide "Unlock" button
        }
    }
}
