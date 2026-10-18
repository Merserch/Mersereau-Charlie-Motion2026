using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    public GameObject player;
    
    void Update()
    {
        transform.position = player.transform.position;
    }
}
