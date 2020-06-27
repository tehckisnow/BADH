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
    public Text mainResource;
    public string mainResourceName;
    
    public Text mainResourceAmount;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = titleTextValue;
        mainResource.text = mainResourceName + ": ";
        mainResourceAmount.text = GetResource(mainResourceName).ToString();
        //gameController = new GameController();

    }

    // Update is called once per frame
    void Update()
    {
        //get data from gameController

        //set ui object string text
mainResource.text = mainResourceName + ": ";
        mainResourceAmount.text = GetResource(mainResourceName).ToString();
    }

    float GetResource(string resourceName)
    {
        return gameController.GetResource(resourceName);
    }

}