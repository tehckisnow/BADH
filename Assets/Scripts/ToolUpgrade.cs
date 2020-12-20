using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[Serializable]
public class ToolUpgrade : MonoBehaviour
{


    public string upgradeName = "upgrade";
    public bool unlocked = false;
    public bool acquired = false;
    public float magnitude = 1;
    public float frequency = 1;
    public Button purchaseButton;
    
    public float cost; //amount of resource spent upon purchase
    public string resourceCost; //type of resource spent upon purchase

    
}
