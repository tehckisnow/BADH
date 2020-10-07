using System.Collections;
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
    public List<GameObject> tools;
    public AudioSource sfx;
    public GameObject upgradesListMarker;
    public Text mainResourceAmountText;

    public TMP_Dropdown toolsDropdown;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = titleTextValue;
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceID).ToString();
        FillToolsDropdown();
    }

    //TODO: This needs to be called elsewhere to update the tools in the dropdown
    //if it is added to Update() then other tools become unselectable
    //because FillToolsDropdown() resets the value of the dropdown to 0
    void FillToolsDropdown()
    {
        int value = toolsDropdown.value;
        toolsDropdown.ClearOptions();
        List<string> names = new List<string>();
        //List<TMP_Dropdown.OptionData>
        
        foreach(GameObject i in tools)
        {
            Tool tool = i.GetComponent<Tool>();
            //check if tool has been unlocked
            if(tool.unlocked)
            {
                string name = "<sprite=" + tool.iconNumber + ">" + i.GetComponent<Tool>().toolName;
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
        float magnitude = currentTool.CalcMagnitude();
        gameController.ModResource(mainResourceID, magnitude);
        //damage text
        GameObject damageTextInstance = Instantiate(damageTextPrefab, mainButton.transform);
        damageTextInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = magnitude.ToString();
    }

    private void SetTool(Tool tool)
    {
        currentTool = tool;
        upgradesListMarker.transform.DetachChildren();
        tool.upgradesUIList.transform.SetParent(upgradesListMarker.transform);

    }

    public void UpdateActiveTool()
    {
        //get value of dropdown
        int val = toolsDropdown.value;
        //get tool
        Tool activeTool = tools.ElementAt(val).GetComponent<Tool>();
        //SetTool()
        SetTool(activeTool);
    }

    public void UnlockTool(Tool tool)
    {
        tool.unlocked = true;
        FillToolsDropdown();
    }

}