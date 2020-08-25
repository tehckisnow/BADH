using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameController : MonoBehaviour
{

    public GameObject barnTab;
    public TextAsset defaultDataFile;

    private JSONNode jsonNode;

    // Start is called before the first frame update
    void Start()
    {
        playerDbCheckandLoad();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetResource(string resourceName)
    {
        return jsonNode["Resources"][resourceName]["value"];
    }

    public void SetResource(string resourceName, float newValue)
    {
        jsonNode["Resources"][resourceName]["value"] = newValue;
    }

    public void ModResource(string resourceName, float newValue)
    {
        jsonNode["Resources"][resourceName]["value"] += newValue;
    }

private void playerDbCheckandLoad()
{
    var fileName = Path.Combine(Application.persistentDataPath, "BADHdb.json");
    if(!File.Exists(fileName))
    {
        Debug.Log("player db does not exist");

        StreamWriter writer = new StreamWriter(fileName, true);
        writer.WriteLine(defaultDataFile.text);
        writer.Close();
    }
        Debug.Log("Load data");

        StreamReader playerReader = new StreamReader(fileName); 
        var playerdata = playerReader.ReadToEnd();
        jsonNode = SimpleJSON.JSON.Parse(playerdata);
        playerReader.Close();

        var defaultJsonNode = SimpleJSON.JSON.Parse(defaultDataFile.text);
        jsonNode = mergeDefaultData(defaultJsonNode, jsonNode);
        Debug.Log(jsonNode);  
}

public static JSONNode mergeDefaultData(JSONNode defaultJson, JSONNode playerJson) 
{

    foreach(var node in defaultJson) {
        if(!playerJson.HasKey(node.Key)){
            playerJson.Add(node.Key, node.Value);
        }else{
            mergeDefaultData(node.Value, playerJson[node.Key]); 
        }
    }

    return playerJson;
}
}