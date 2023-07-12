using UnityEngine;

public class RotatePlayers : MonoBehaviour
{
    public GameManager gm;
    public float rotationSpeed = 1f;

    private void Update()
    {
        // Rotate the center GameObject
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    public void ChangeCharacter(int newIndex) {
        Debug.Log("ChangeCharacter: " + newIndex);

        gm.players[gm.playerIdx].SetActive(false);
        gm.playerParent.SetActive(false);
        
        gm.players[newIndex].SetActive(true);
        gm.player = gm.players[newIndex].GetComponent<Player>();
        gm.playerParent = gm.player.transform.parent.gameObject;
        gm.playerParent.SetActive(true);

        gm.playerIdx = newIndex;
    }
}
