using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [Header("Labels")]
    [SerializeField] Text playerMoneyTxt;
    [SerializeField] Text[] cropCounterTxt;
    [SerializeField] Text[] playerCropQtyShopTxt;
    [SerializeField] Text[] cropPriceShopTxt;

    [Header("Shop Interface")]
    [SerializeField] GameObject leftColumnPage1;
    [SerializeField] GameObject rightColumnPage1;
    [SerializeField] GameObject leftColumnPage2;
    [SerializeField] Button changePageBtn;

    [Header("Header")]
    [SerializeField] Image currentSelectedCrop;
    [SerializeField] Text currentModeLbl;
    [SerializeField] GameObject switchModeBtn;

    [SerializeField] GameObject[] cropButtons;
    [SerializeField] Player player;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject cropsTreePanel;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void DisplayUnlockedButtons()
    {
        foreach (GameObject cropButton in cropButtons)
        {
            int cropId = cropButton.GetComponent<CropButton>().GetCropId();
            bool isUnlocked = gm.cropsAvailable[(GameManager.CROPS)cropId];
            cropButton.SetActive(isUnlocked);
        }
    }

    void Update()
    {
        UpdateHeaderUI();
        UpdateUIPlayerCrops();
        DisplayUnlockedButtons();

        if (Input.GetKeyDown(KeyCode.S))
        {
            shopPanel.SetActive(!shopPanel.activeInHierarchy);
        }

        if (gm.isPlanting)
        {
            switchModeBtn.SetActive(true);
        } else
        {
            switchModeBtn.SetActive(false);
        }
    }

    private void UpdateHeaderUI()
    {
        playerMoneyTxt.text = player.GetMoney().ToString() + " �";

        if (gm.GetSelectedCrop() != null && gm.isPlanting)
        {
            currentModeLbl.text = "Mode : Plantation de ";
            currentSelectedCrop.sprite = gm.GetSelectedCrop().GetComponent<Crop>().illustration;
            currentSelectedCrop.color = new Color(1, 1, 1, 1);
        } 
        else
        {
            currentModeLbl.text = "Mode : R�colte";
            currentSelectedCrop.sprite = null;
            currentSelectedCrop.color = new Color(1, 1, 1, 0);
        }
    }

    private void UpdateUIPlayerCrops()
    {
        if (player.crops != null)
        {
            for (int i = 0; i < 20; i++)
            {
                string qtyToDisplay = player.crops[Enum.GetName(typeof(GameManager.CROPS), (GameManager.CROPS)i)].ToString();
                cropCounterTxt[i].text = qtyToDisplay;
                playerCropQtyShopTxt[i].text = qtyToDisplay;
                cropPriceShopTxt[i].text = gm.cropPrices[i].ToString() + " �";
            }
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OpenCropsTree()
    {
        cropsTreePanel.SetActive(true);
    }

    public void CloseCropsTree()
    {
        cropsTreePanel.SetActive(false);
    }

    public void DisplaySecondPage()
    {
        leftColumnPage1.SetActive(false);
        rightColumnPage1.SetActive(false);
        leftColumnPage2.SetActive(true);

        changePageBtn.GetComponentInChildren<Text>().text = "Page 1";
        changePageBtn.onClick.RemoveAllListeners();
        changePageBtn.onClick.AddListener(delegate { DisplayFirstPage(); });
    }

    public void DisplayFirstPage()
    {
        leftColumnPage1.SetActive(true);
        rightColumnPage1.SetActive(true);
        leftColumnPage2.SetActive(false);

        changePageBtn.GetComponentInChildren<Text>().text = "Page 2";
        changePageBtn.onClick.RemoveAllListeners();
        changePageBtn.onClick.AddListener(delegate { DisplaySecondPage(); });
    }
}
