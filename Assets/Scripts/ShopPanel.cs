using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] GameObject[] shopRows;
    GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
        DisplayShopRowIfUnlocked();
    }

    private void DisplayShopRowIfUnlocked()
    {
        for (int i = 0; i < 20; i++)
        {
            if (gm.cropsAvailable[(GameManager.CROPS)i])
            {
                shopRows[i].SetActive(true);
            }
            else
            {
                shopRows[i].SetActive(false);
            }
        }
    }
}
