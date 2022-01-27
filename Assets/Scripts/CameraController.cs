using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Define player game object
    public GameObject Player;

    // Wait for lateupdate
    void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, 3.5f, Player.transform.position.z -5);
    }
}