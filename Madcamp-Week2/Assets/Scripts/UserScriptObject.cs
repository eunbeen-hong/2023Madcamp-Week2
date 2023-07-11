using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UserScriptObject", order = 1)]
public class UserScriptObject : ScriptableObject
{
    public string id;
    public string username;
    public string univ;
    public int bestScore;
}