using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : MonoBehaviour
{
    [ContextMenu("test get")]
    public async void TestGet()
    {
        var url = "";

        using var www = UnityWebRequest.Get(url);

        www.SetRequestHeader("Content-Type", "application/json");

        var operation = www.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        var jsonResponse = www.downloadHandler.text;

        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError($"Failed: {www.error}");

        try
        {
            var result = JsonConvert.DeserializeObject<Grid>(jsonResponse);
            Debug.Log($"Succes : {www.downloadHandler.text}");
        }
        catch(Exception ex)
        {
            Debug.LogError($"{this} could not parse json {jsonResponse}. {ex.Message}");
        }
    }
}
