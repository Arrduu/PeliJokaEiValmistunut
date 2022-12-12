using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendHandler : MonoBehaviour
{    
    int fetchCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BackendHandler started");

        hs = JsonUtility.FromJson<HighScores.HighScores>(jsonTestStr);
        Debug.Log("HighScores name: " + hs.scores[0].playername);
        Debug.Log("HighScores as json: " + JsonUtility.ToJson(hs));
    }
    const string jsonTestStr = "{ " +
        "\"scores\":[ " +
        "{\"id\":1, \"playername\":\"Matti\", \"score\":20, \"playtime\": \"2020-21-11 08:20:00\"}, " +
        "{\"id\":2, \"playername\":\"Hankka\", \"score\":30, \"playtime\": \"2020-21-11 08:20:00\"}, " +
        "{\"id\":3, \"playername\":\"Ismo\", \"score\":40, \"playtime\": \"2020-21-11 08:20:00\"} " +
        "] }";
    // vars
    HighScores.HighScores hs;

    public void FetchHighScoresJSONFile()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSONFile button clicked");
    }
    public void FetchHighScoresJSON()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSON button clicked");
    }



}
