using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Dictionary<string, int> crops = new Dictionary<string, int>();
    private float money = 100;

    void Start()
    {
        foreach(string name in Enum.GetNames(typeof(GameManager.CROPS)))
        {
            crops[name] = 0;
        }

        crops[Enum.GetName(typeof(GameManager.CROPS), GameManager.CROPS.TURNIP)] = 5;
    }

    public void RemoveCrop(GameManager.CROPS crop, int qty)
    {
        crops[Enum.GetName(typeof(GameManager.CROPS), crop)] -= qty;
    }

    public void AddCrop(GameManager.CROPS crop, int qty)
    {
        crops[Enum.GetName(typeof(GameManager.CROPS), crop)] += qty;
    }

    public float GetMoney()
    {
        return money;
    }

    public void SetMoney(float _money)
    {
        money = _money;
    }
}
