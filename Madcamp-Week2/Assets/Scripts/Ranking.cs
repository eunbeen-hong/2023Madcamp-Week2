using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct User
{
    public string school;
    public string username;
    public int score;

    public User(string school, string username, int score)
    {
        this.school = school;
        this.username = username;
        this.score = score;
    }
}


public class Ranking : MonoBehaviour
{
    GameManager gameManager;
    public GameObject First, Second, Third;
    public Animator anim;
    public GameObject rankingPannel;
    private SpriteRenderer spriteRenderer;

    private string[] SchoolNames = {"Others", "GIST", "한양대학교", "KAIST", "고려대학교", "성균관대학교", "숙명여자대학교", "POSTHECH"};
    
    User[] rankings = new User[3];
    
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Start() {
        rankingPannel = GameObject.Find("RankingList");
        
        // get ranking from server

        rankings[0] = new User("GIST", "김지원", 100);
        rankings[1] = new User("한양대학교", "김지원", 200);
        rankings[2] = new User("KAIST", "김지원", 200);
        
        string firstSchoolName = SchoolNameConverter(rankings[0].school);
        string secondSchoolName = SchoolNameConverter(rankings[1].school);
        string thirdSchoolName = SchoolNameConverter(rankings[2].school);

        Transform firstSchool = transform.Find("First");
        Transform secondSchool = transform.Find("Second");
        Transform thirdSchool = transform.Find("Third");
        
        SetPlayerActive(firstSchool, firstSchoolName);
        SetPlayerActive(secondSchool, secondSchoolName);
        SetPlayerActive(thirdSchool, thirdSchoolName);

        LoadRanking();
    }

    private void SetPlayerActive(Transform parentTransform, string targetPlayerName) {
        if (parentTransform != null) {
            for (int i = 0; i< parentTransform.childCount; i++) {
                Transform currentChild = parentTransform.GetChild(i);

                if (currentChild.name == targetPlayerName) {
                    currentChild.gameObject.SetActive(true);
                    break;
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
            case "한양대학교":
                return "hanPlayer";
            case "KAIST":
                return "kaPlayer";
            case "고려대학교":
                return "koPlayer";
            case "성균관대학교":
                return "sungPlayer";
            case "숙명여자대학교":
                return "sukPlayer";
            case "POSTHECH":
                return "poPlayer";
            default:
                return "Default1";
        }
    }

    public void LoadRanking() {
        GameObject playerPrefab = Resources.Load<GameObject>("PlayerItem");
        foreach (User user in rankings) {
            GameObject newItem = Instantiate(playerPrefab);
            newItem.transform.GetChild(0).GetComponent<TMP_Text>().text = user.school;
            newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = user.username;
            newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = user.score.ToString();

            newItem.transform.SetParent(rankingPannel.transform, false);
            newItem.transform.localScale = Vector2.one;
        }
    }
}
