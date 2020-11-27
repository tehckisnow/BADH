using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[Serializable]
public class Tool : MonoBehaviour
{
    public string toolName = "tool";
    public int iconNumber = 0;
    public bool unlocked = false;
    public bool acquired = false;
    public float magnitude = 1;
    public float frequency = 1;
    public List<ToolUpgrade> upgrades;
    public GameObject upgradesUIList;

    public float cost = 100; //amount of resource spent upon purchase
    public string resourceCost = "clout"; //type of resource spent upon purchase
    public string statsDisplay = "mag: +1";
    
}
