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

    public string urlGetRank;
    public string urlPostById;
    public string urlUpdateScore;

    public UserScriptObject scriptableObject;

    public Button btnGetRank;
    public Button btnPostById;
    public Button btnUpdateScore;

    void Start()
    {
        this.btnGetRank.onClick.AddListener(() => {
            // var url = string.Format("{0}:{1}/{2}", host, port, urlGetAll);
            var url = string.Format("{0}/{1}", host, urlGetRank);
            Debug.Log(url);

            StartCoroutine(this.GetRank(url, (raw) =>
            {
                User[] res = JsonConvert.DeserializeObject<User[]>(raw);
                
                Debug.LogFormat("GetAll Result:\n");
                foreach (User user in res)
                {
                    Debug.LogFormat("{0} : {1}", user.username, user.bestScore);
                }

                // TODO: Sort하기 (key: bestScore)
            }));
        });

        this.btnPostById.onClick.AddListener(() => {
            // var url = string.Format("{0}:{1}/{2}", host, port, urlGetScore);
            var url = string.Format("{0}/{1}", host, urlPostById);
            Debug.Log(url);

            var req = new Protocols.Packets.req_PostById();
            req.id = scriptableObject.id;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(this.GetScore(url, json, (raw) =>
            {
                User res = JsonConvert.DeserializeObject<User>(raw);
                Debug.LogFormat("Post Result: {0} : {1}", res.username, res.bestScore);
            }));
        });

        this.btnUpdateScore.onClick.AddListener(() => {
            // var url = string.Format("{0}:{1}/{2}", host, port, urlUpdateScore);
            var url = string.Format("{0}/{1}", host, urlUpdateScore);
            Debug.Log(url);

            var req = new Protocols.Packets.req_UpdateUser();
            req.id = scriptableObject.id;
            req.bestScore = scriptableObject.bestScore;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(this.UpdateScore(url, json, (raw) => {
                // var res = JsonConvert.DeserializeObject<Protocols.Packets.res>(raw);
                Debug.LogFormat("UPDATED {0} -> {1}", req.username, req.bestScore);

            }));
        });
    }



    private IEnumerator GetRank(string url, System.Action<string> callback)
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
    
    private IEnumerator PostScoreById(string url, string json, System.Action<string> callback) {
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