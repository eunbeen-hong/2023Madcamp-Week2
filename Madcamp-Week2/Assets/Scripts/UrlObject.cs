using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UrlObject", order = 1)]
public class UrlObject : ScriptableObject
{
    public string host;
    public string urlGetRank;
    public string urlPostById;
    public string urlUpdateScore;
}