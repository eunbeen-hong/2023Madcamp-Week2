using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System;


public class Ranking : MonoBehaviour
{
    GameManager gameManager;
    public GameObject First, Second, Third;
    public Animator anim;
    public GameObject rankingView, myBest;
    private SpriteRenderer spriteRenderer;

    private string[] SchoolNames = {"Others", "GIST", "한양대학교", "KAIST", "고려대학교", "성균관대학교", "숙명여자대학교", "POSTHECH"};
    
    public UserScriptObject userObj;
    public UrlObject URL;
    _User[] rankings = new _User[80];

    int length = 0;
    
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();  
    }

    void Start() {
        rankingView = GameObject.Find("Content");

        var url = string.Format("{0}/{1}", URL.host, URL.urlGetRank);
        Debug.Log(url);

        StartCoroutine(RankMain.GetRank(url, (raw) =>
        {
            _User[] res = JsonConvert.DeserializeObject<_User[]>(raw);
        
            Debug.LogFormat("GetAll Result:\n");

            Array.Sort(res, (x, y) => y.bestScore.CompareTo(x.bestScore));

            int i = 0;
            foreach (_User user in res)
            {
                if (i >= 80) {
                    break;
                }
                Debug.LogFormat("{0} : {1}", user.username, user.bestScore);
                rankings[i] = new _User();
                rankings[i].username = user.username;
                rankings[i].id = user.id;
                rankings[i].bestScore = user.bestScore;
                rankings[i].univ = user.univ;

                i++; length++; 
            }
        }));

        Invoke("showRank", 2);
    }

    void showRank () {
        try {
        myBest.GetComponent<TMP_Text>().text = userObj.bestScore.ToString();

        string firstSchoolName = SchoolNameConverter(rankings[0].univ);
        string secondSchoolName = SchoolNameConverter(rankings[1].univ);
        string thirdSchoolName = SchoolNameConverter(rankings[2].univ);

        // string firstSchoolName = SchoolNameConverter("GIST");
        // string secondSchoolName = SchoolNameConverter("한양대학교");
        // string thirdSchoolName = SchoolNameConverter("KAIST");

        Transform firstSchool = transform.Find("First");
        Transform secondSchool = transform.Find("Second");
        Transform thirdSchool = transform.Find("Third");
        
        SetPlayerActive(firstSchool, rankings[0].univ);
        SetPlayerActive(secondSchool, rankings[1].univ);
        SetPlayerActive(thirdSchool, rankings[2].univ);

        LoadRanking();
        } catch (Exception _) { // NullReferenceException
            Debug.Log("현재 랭킹탭에 접근할 수 없습니다.");
        }
    }

    void Update() {

    }

    private void SetPlayerActive(Transform parentTransform, string school) {
        string targetSchoolName = SchoolNameConverter(school);
        if (parentTransform != null) {
            for (int i = 0; i< parentTransform.childCount; i++) {
                Transform currentChild = parentTransform.GetChild(i);

                if (currentChild.name == targetSchoolName) {
                    currentChild.gameObject.SetActive(true);
                    break;
                } else if (currentChild.name == "SchoolName") {
                    currentChild.GetComponent<TMP_Text>().text = school;
                }
            }
        } else {
            Debug.Log("parentTransform is null");
        }

    }

    private string SchoolNameConverter(string SchoolName) {
        switch (SchoolName) {
            case "GIST":
                return "gistPlayer";
            case "한양대":
                return "hanPlayer";
            case "KAIST":
                return "kaPlayer";
            case "고려대":
                return "koPlayer";
            case "성균관대":
                return "sungPlayer";
            case "숙명여대":
                return "sukPlayer";
            case "POSTECH":
                return "poPlayer";
            case "Default1":
                return "Default1";
            default:
                return "Default1";
        }
    }

    public void LoadRanking() {
        GameObject playerPrefab = Resources.Load<GameObject>("PlayerItem");
        for (int i = 0; i<length; i++) {

            _User user = rankings[i];
            GameObject newItem = Instantiate(playerPrefab);
            newItem.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
            newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = user.univ;
            newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = user.username;
            newItem.transform.GetChild(3).GetComponent<TMP_Text>().text = user.bestScore.ToString();

            newItem.transform.SetParent(rankingView.transform, false);
            newItem.transform.localScale = Vector2.one;
        }
    }
}
