using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;


public class RankMain : MonoBehaviour
{
    public UserScriptObject scriptableObject;

    public UrlObject URL;

    public Button btnGetRank;
    public Button btnPostById;
    public Button btnUpdateScore;

    void Start()
    {
        this.btnGetRank.onClick.AddListener(() => {
            // var url = string.Format("{0}:{1}/{2}", host, port, urlGetAll);
            var url = string.Format("{0}/{1}", URL.host, URL.urlGetRank);
            Debug.Log(url);

            StartCoroutine(RankMain.GetRank(url, (raw) =>
            {
                _User[] res = JsonConvert.DeserializeObject<_User[]>(raw);
                
                Debug.LogFormat("GetAll Result:\n");
                foreach (_User user in res)
                {
                    Debug.LogFormat("{0} : {1}", user.username, user.bestScore);
                }

                // TODO: Sort하기 (key: bestScore)
                
            }));
        });

        this.btnPostById.onClick.AddListener(() => {
            // var url = string.Format("{0}:{1}/{2}", host, port, urlGetScore);
            var url = string.Format("{0}/{1}", URL.host, URL.urlPostById);
            Debug.Log(url);

            var req = new Protocols.Packets.req_PostById();
            req.id = scriptableObject.id;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(RankMain.PostScoreById(url, json, (raw) =>
            {
                _User res = JsonConvert.DeserializeObject<_User>(raw);
                Debug.LogFormat("Post Result: {0} : {1}", res.username, res.bestScore);
            }));
        });

        this.btnUpdateScore.onClick.AddListener(() => {
            // var url = string.Format("{0}:{1}/{2}", host, port, urlUpdateScore);
            var url = string.Format("{0}/{1}", URL.host, URL.urlUpdateScore);
            Debug.Log(url);

            var req = new Protocols.Packets.req_UpdateUser();
            req.id = scriptableObject.id;
            req.bestScore = scriptableObject.bestScore;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(RankMain.UpdateScore(url, json, (raw) => {
                // var res = JsonConvert.DeserializeObject<Protocols.Packets.res>(raw);
                Debug.LogFormat("UPDATED {0} -> {1}", req.id, req.bestScore);

            }));
        });
    }



    public static IEnumerator GetRank(string url, System.Action<string> callback)
    {
        var webRequest = new UnityWebRequest(url, "GET");
        Debug.Log(url);

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
    
    public static IEnumerator PostScoreById(string url, string json, System.Action<string> callback) {
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

    public static IEnumerator UpdateScore(string url, string json, System.Action<string> callback)
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