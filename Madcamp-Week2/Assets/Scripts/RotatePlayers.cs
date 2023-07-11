using UnityEngine;

public class RotatePlayers : MonoBehaviour
{
    public float rotationSpeed = 1f;

    private void Update()
    {
        // Rotate the center GameObject
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
