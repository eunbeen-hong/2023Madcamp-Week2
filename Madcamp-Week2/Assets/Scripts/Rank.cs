using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;

using Newtonsoft.Json;

public class RankMain : MonoBehaviour
{
    public string host;
    public int port;

    public string urlGetAll;
    public string urlGetScore;
    public string urlInsertUser;
    public string urlUpdateScore;

    public SpawnManagerScriptableObject scriptableObject;

    public Button btnGetAll;
    public Button btnGetScore;
    public Button btnInsertUser;
    public Button btnUpdateScore;

    void Start()
    {
        this.btnGetAll.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, urlGetAll);
            Debug.Log(url);

            StartCoroutine(this.GetAll(url, (raw) =>
            {
                // var anonymousObject = new { username = "", profilePic = "", bestScore = 0 };
                // var deserializedObject = JsonConvert.DeserializeAnonymousType(jsonString, anonymousObject);
                // var user = new User {
                //     username = deserializedObject.username,
                //     profilePic = deserializedObject.profilePic,
                //     bestScore = deserializedObject.bestScore
                // };
                User[] res = JsonConvert.DeserializeObject<User[]>(raw);
                
                // User[] res = JsonConvert.DeserializeObject<Protocols.Packets.res_GetAll>(raw);
                Debug.LogFormat("GetAll Result:\n");
                foreach (User user in res)
                {
                    Debug.LogFormat("{0} : {1}", user.username, user.bestScore);
                }
            }));
        });

        this.btnGetScore.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, urlGetScore);
            Debug.Log(url);

            var req = new Protocols.Packets.req_GetScore();
            req.username = scriptableObject.username;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(this.GetScore(url, json, (raw) =>
            {
                var res = int.Parse(raw);
                Debug.LogFormat("GetScore Result: {0} : {1}", req.username, res);
            }));
        });

        this.btnInsertUser.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, urlInsertUser);
            Debug.Log(url);

            var req = new Protocols.Packets.req_InsertUser();
            req.username = scriptableObject.username;
            req.profilePic = scriptableObject.profilePic;
            req.bestScore = scriptableObject.bestScore.ToString();
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(this.InsertUser(url, json, (raw) =>
            {
                // var res = JsonConvert.DeserializeObject<Protocols.Packets.req_InsertUser>(raw);
                Debug.LogFormat("INSERTED {0}, {1}, {2}", req.username, req.profilePic, req.bestScore);
            }));
        });

        this.btnUpdateScore.onClick.AddListener(() => {
            var url = string.Format("{0}:{1}/{2}", host, port, urlUpdateScore);
            Debug.Log(url);

            var req = new Protocols.Packets.req_UpdateUser();
            req.username = scriptableObject.username;
            req.bestScore = scriptableObject.bestScore;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(this.UpdateScore(url, json, (raw) => {
                // var res = JsonConvert.DeserializeObject<Protocols.Packets.res>(raw);
                Debug.LogFormat("UPDATED {0} -> {1}", req.username, req.bestScore);

            }));
        });
    }

    private IEnumerator GetAll(string url, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "GET");

        webRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }
    
    private IEnumerator GetScore(string url, string json, System.Action<string> callback) {
        var webRequest = new UnityWebRequest(url, "GET");
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator InsertUser(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

    private IEnumerator UpdateScore(string url, string json, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
        }
        else
        {
            Debug.LogFormat("{0}\n{1}\n{2}", webRequest.responseCode, webRequest.downloadHandler.data, webRequest.downloadHandler.text);
            callback(webRequest.downloadHandler.text);
        }
    }

}