using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameController : MonoBehaviour
{
    public bool scorchedEarth;
    public GameObject barnTab;
    public TextAsset defaultDataFile;

    private JSONNode jsonNode;
    //private List<Tab> tabs;

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
        savePlayerDatabase();
    }

    // public List<string> GetTools(string tab){
    //     tabs.Where(t => t.id == tab).Select(n => n.tools);
    // }

    private void playerDbCheckandLoad()
    {
        var fileName = Path.Combine(Application.persistentDataPath, "BADHdb.json");
        UnityEngine.Debug.Log(fileName);
        if (!File.Exists(fileName) || scorchedEarth)
        {
            jsonNode = SimpleJSON.JSON.Parse(defaultDataFile.text);
            savePlayerDatabase();
        }
        //   UnityEngine.Debug.Log("Load data");

        StreamReader playerReader = new StreamReader(fileName);
        var playerdata = playerReader.ReadToEnd();
        jsonNode = SimpleJSON.JSON.Parse(playerdata);
        playerReader.Close();

        var defaultJsonNode = SimpleJSON.JSON.Parse(defaultDataFile.text);

        var defaultVersionNum = defaultJsonNode["Version"];
        var playerVersionNum = jsonNode["Version"];
        if (defaultVersionNum != playerVersionNum)
        {
            jsonNode = mergeDefaultData(defaultJsonNode, jsonNode);
            jsonNode = deleteExtraneousPlayerData(defaultJsonNode, jsonNode);
        }
        //  UnityEngine.Debug.Log(jsonNode);  
    }

    public static JSONNode mergeDefaultData(JSONNode defaultJson, JSONNode playerJson)
    {

        foreach (var node in defaultJson)
        {
            if (!playerJson.HasKey(node.Key))
            {
                playerJson.Add(node.Key, node.Value);
            }
            else
            {
                mergeDefaultData(node.Value, playerJson[node.Key]);
                if (node.Key != "resourceAmount")
                {
                    playerJson[node.Key].Value = node.Value;
                }
            }
        }

        return playerJson;
    }

    public static JSONNode deleteExtraneousPlayerData(JSONNode defaultJson, JSONNode playerJson)
    {

        foreach (var node in playerJson)
        {
            if (!defaultJson.HasKey(node.Key))
            {
                playerJson.Remove(node.Key);
            }
            else
            {
                deleteExtraneousPlayerData(node.Value, playerJson[node.Key]);
            }
        }

        return playerJson;
    }
    public void savePlayerDatabase()
    {
        var fileName = Path.Combine(Application.persistentDataPath, "BADHdb.json");

        StreamWriter writer = new StreamWriter(fileName, false);
        writer.WriteLine(jsonNode.ToString());
        writer.Close();
    }

}