using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject barnTab;

    Dictionary<string, float> playerResources = new Dictionary<string, float>()
    {
        {"clout", 10},
        {"horse meat", 0},
        {"acreage", 1},
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetResource(string resourceName)
    {
        return playerResources[resourceName];
    }

    public void SetResource(string resourceName, float newValue)
    {
        playerResources[resourceName] = newValue;
    }
}