﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolUpgrade : MonoBehaviour
{
    public string upgradeName = "upgrade";
    public bool unlocked = false;
    public bool acquired = false;
    public float magnitude = 1;
    public float frequency = 1;
    public Button purchaseButton;
    public GameController gameController;

    public float cost;
    public string resourceCost;

    public void Start()
    {
        purchaseButton.transform.Find("CostText").gameObject.GetComponent<TextMeshProUGUI>().text = cost.ToString() + " " + resourceCost;
    }

    public void Purchase()
    {
        //disable button
        purchaseButton.interactable = false;
        //toggle acquired
        acquired = true;
        //remove price
        purchaseButton.transform.Find("CostText").gameObject.SetActive(false);
        //other indicator (shading?)
        purchaseButton.GetComponent<Image>().color = Color.gray;
        //subtract cost
        gameController.ModResource(resourceCost, -cost);

    }
    
}
