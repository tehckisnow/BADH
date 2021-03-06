﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabController : MonoBehaviour
{
    public Canvas canvas;
    public GameObject damageTextPrefab;
    public GameObject mainButton;
    public GameController gameController;
    //public reference to ui object
    public Text titleText;
    public string titleTextValue;
    public Text mainResourceText;
    public string mainResourceID;
    public string mainResourceTextValue;
    public Animator buttonAnimator;
    public Tool currentTool;
    private int currentToolVal = 0;
    public List<GameObject> tools;
    public AudioSource sfx;
    public GameObject upgradesListMarker;
    public Text mainResourceAmountText;

    public TMP_Dropdown toolsDropdown;
    public GameObject upgradesUIList;
    public GameObject upgradePrefab;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = titleTextValue;
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceID).ToString();
        FillToolsDropdown();
        //set starting tool
        SetTool(tools[0].GetComponent<Tool>());
        //InitUpgrades()
        foreach(GameObject currentTool in tools)
        {
            foreach(ToolUpgrade currentUpgrade in currentTool.GetComponent<Tool>().upgrades)
            {
                UnityEngine.Events.UnityAction action = () => { this.PurchaseUpgrade(currentUpgrade); };
                currentUpgrade.gameObject.GetComponent<Button>().onClick.AddListener(action);
            }
        }
    }

    //TODO: This needs to be called elsewhere to update the tools in the dropdown
    //if it is added to Update() then other tools become unselectable
    //because FillToolsDropdown() resets the value of the dropdown to 0
    public void FillToolsDropdown()
    {
        int value = toolsDropdown.value;
        toolsDropdown.ClearOptions();
        List<string> names = new List<string>();
        //List<TMP_Dropdown.OptionData>

        foreach (GameObject i in tools)
        {
            Tool tool = i.GetComponent<Tool>();
            //check if tool has been unlocked
            if (tool.unlocked)
            {
                string name = "<sprite=" + tool.iconNumber + ">" + tool.toolName;
                if (!tool.acquired)
                {
                    name += " (" + tool.cost + " " + tool.resourceCost + ")";
                }
                name += "\n" + tool.statsDisplay;
                names.Add(name);
            }
        }

        toolsDropdown.AddOptions(names);
        UpdateActiveTool();
        //remember previous value
        toolsDropdown.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        //get data from gameController

        //set ui object string text
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceID).ToString();

        //! should this be called here?
        //fillToolsDropdown();
    }

    float GetResource(string resourceName)
    {
        return gameController.GetResource(resourceName);
    }

    public void TapMainButton()
    {
        //buttonAnimator.SetBool("mainButtonReady", false);
        buttonAnimator.Play("beatHorse");
        sfx.Play();
        float magnitude = CalcToolMagnitude(currentTool);
        //! ADD CHECK BELOW?
        gameController.ModResource(mainResourceID, magnitude);
        //damage text
        GameObject damageTextInstance = Instantiate(damageTextPrefab, mainButton.transform);
        damageTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = magnitude.ToString();
    }

    #region Tools
    private void SetTool(Tool tool)
    {
        PopulateUpgradeList(tool.upgrades);
        currentTool = tool;
        currentToolVal = toolsDropdown.value;
    }

    public void PopulateUpgradeList(List<ToolUpgrade> upgrades)
    {

        var gridContent = upgradesUIList.transform.Find("GridContent").transform;
        //foreach (Transform child in gridContent)
        for(int i = gridContent.childCount - 1; i >= 0; --i)
        {
            //Destroy(child.gameObject);
            Transform currentChild = gridContent.GetChild(i);
            currentChild.SetParent(currentTool.transform);
            //child.SetParent(currentTool.transform);
            //child.gameObject.SetActive(false);
            currentChild.gameObject.SetActive(false);

        }

        foreach (var upgrade in upgrades)
        {
            //GameObject newUpgrade = (GameObject)Instantiate(upgradePrefab, gridContent);
            GameObject newUpgrade = upgrade.gameObject;
            newUpgrade.gameObject.SetActive(true);
            newUpgrade.transform.SetParent(gridContent);
        }
    }

    public void UpdateActiveTool()
    {
        //get value of dropdown
        int val = toolsDropdown.value;
        //get tool
        Tool activeTool = tools.ElementAt(val).GetComponent<Tool>();
        //check for purchase first time
        if (!activeTool.acquired)
        {
            if(PurchaseTool(activeTool))
            {
                SetTool(activeTool);
            }
            else
            {
                toolsDropdown.value = currentToolVal;
            }
        }
        else
        SetTool(activeTool);
    }

    public void UnlockTool(Tool tool)
    {
        tool.unlocked = true;
        FillToolsDropdown();
    }

    public float CalcToolMagnitude(Tool Tool)
    {
        float currentMagnitude = Tool.magnitude;
        foreach (ToolUpgrade upgrade in Tool.upgrades)
        {
            if (upgrade.acquired)
            {
                currentMagnitude += upgrade.magnitude;
            }
        }
        return currentMagnitude;
    }

    public bool PurchaseTool(Tool tool)
    {
        bool ret = false;

        if(gameController.ModResource(tool.resourceCost, -tool.cost))
        {
            //change state to indicate has been purchased
            tool.acquired = true;
            //call FillToolsDropdown from TabController
            FillToolsDropdown();
            ret = true;
        }
        return ret;
    }
    #endregion Tools

    #region Upgrades
    public void PurchaseUpgrade(ToolUpgrade upgrade)
    {
        //disable button
        upgrade.purchaseButton.interactable = false;
        //subtract cost
        if(gameController.ModResource(upgrade.resourceCost, -upgrade.cost))
        {
            //toggle acquired
            upgrade.acquired = true;
            //remove price
            upgrade.purchaseButton.transform.Find("CostText").gameObject.SetActive(false);
            //other indicator (shading?)
            upgrade.purchaseButton.GetComponent<Image>().color = Color.gray;
            //!check if cost can be covered
        }
        else
        {
            upgrade.purchaseButton.interactable = true;
        }

    }

    #endregion Upgrades

}