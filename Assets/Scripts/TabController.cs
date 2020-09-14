using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public GameController gameController;
    //public reference to ui object
    public Text titleText;
    public string titleTextValue;
    public Text mainResourceText;
    public string mainResourceID;
    public string mainResourceTextValue;
    public Animator buttonAnimator;
    public Tool currentTool;
    public AudioSource sfx;

    public Text mainResourceAmountText;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = titleTextValue;
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceID).ToString();
        NewTool("bah");
    }

    // Update is called once per frame
    void Update()
    {
        //get data from gameController

        //set ui object string text
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceID).ToString();
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
        gameController.ModResource(mainResourceID, currentTool.CalcMagnitude());
        Debug.Log(gameController.GetResource("clout"));
    }

    private void SetTool(Tool tool)
    {
        currentTool = tool;
    }

    private void NewTool(string toolName)//, string iconFilename)
    {
        //!return a gameobject?

        GameObject newTool = new GameObject();
        newTool.name = toolName;
        Image upgradeBG = newTool.AddComponent<Image>() as Image;
        upgradeBG.sprite = Resources.Load<Sprite>("Square");
        //!why can't I see this object in unity?
        
        //Image icon = newTool.AddComponent<Image>();
        //icon.sprite = Resources.Load<Sprite>(iconFilename);
    }

}