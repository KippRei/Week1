using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float cameraSpeed = 2f;
    public float deadzone = 2f;
    public Collider2D bossZone;
    public bool enteredBossZone = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > deadzone || Mathf.Abs(transform.position.y - player.transform.position.y) > deadzone && !enteredBossZone)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), cameraSpeed);
        }
        /*        if (transform.position != player.transform.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), cameraSpeed);
                }*/
        if (enteredBossZone)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(16.9f, 12.5f, -10), cameraSpeed);
        }
    }
}
