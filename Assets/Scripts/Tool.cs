﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    public float CalcMagnitude()
    {
        float currentMagnitude = magnitude;
        foreach(ToolUpgrade upgrade in upgrades)
        {
            if(upgrade.acquired)
            {
                currentMagnitude += upgrade.magnitude;
            }
        }
        return currentMagnitude;
    }
}
