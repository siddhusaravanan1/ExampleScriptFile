using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;

    float newX;
    // Update is called once per frame
    void LateUpdate()
    {
        newX = player.transform.position.x;
        if (newX < -19.78f)
        {
            newX = -19.79f;
        }
        if (newX > 19.6)
        {
            newX = 19.7f;
        }

        transform.position = new Vector3(newX, player.transform.position.y, transform.position.z);
    }
}