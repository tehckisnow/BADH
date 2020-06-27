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
    public string mainResourceTextValue;
    
    public Text mainResourceAmountText;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = titleTextValue;
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceTextValue).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //get data from gameController

        //set ui object string text
        mainResourceText.text = mainResourceTextValue + ": ";
        mainResourceAmountText.text = GetResource(mainResourceTextValue).ToString();
    }

    float GetResource(string resourceName)
    {
        return gameController.GetResource(resourceName);
    }

}