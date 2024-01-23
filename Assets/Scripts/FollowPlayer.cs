using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public Player player;
    public float cameraSpeed = 2f;
    public float deadzone = 2f;
    public Collider2D bossZone;
    public bool enteredBossZone = false;
    public bool bossAlive = true;
    public float lerpTime = 0.3f;
    public float lookAheadDist = 2f; // How far the camera moves in direction of player movement

    private Vector3 bossCamPos = new Vector3(16.9f, 12.5f, -10);
    private bool cameraLeft = false;
    private bool cameraRight = false;
    private bool cameraUp = false;
    private bool cameraDown = false;
    private float xDistToTravel;
    private float yDistToTravel;
    private bool freeMovement = true;


    private float startTime;

    // Update is called once per frame
    void LateUpdate()
    {
        /*        if (Mathf.Abs(transform.position.x - player.gameObject.transform.position.x) > deadzone || Mathf.Abs(transform.position.y - player.gameObject.transform.position.y) > deadzone)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, -10), cameraSpeed);
                }*/
        if (freeMovement)
        {
            if (Input.GetButtonDown("left"))
            {
                cameraRight = false;
                cameraLeft = true;
                xDistToTravel = player.gameObject.transform.localScale.x + lookAheadDist;
                startTime = Time.time;
            }
            else if (Input.GetButtonDown("right"))
            {
                cameraLeft = false;
                cameraRight = true;
                xDistToTravel = player.gameObject.transform.localScale.x + lookAheadDist;
                startTime = Time.time;
            }

            if (Input.GetButtonDown("up"))
            {
                cameraDown = false;
                cameraUp = true;
                yDistToTravel = player.gameObject.transform.localScale.y + lookAheadDist;
                startTime = Time.time;
            }
            else if (Input.GetButtonDown("down"))
            {
                cameraUp = false;
                cameraDown = true;
                yDistToTravel = player.gameObject.transform.localScale.y + lookAheadDist;
                startTime = Time.time;
            }

            if (cameraLeft && transform.position.x > player.gameObject.transform.position.x - lookAheadDist)
            {
                float distCovered = (Time.time - startTime) * lerpTime;
                float fractionOfJourney = distCovered / xDistToTravel;
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.gameObject.transform.position.x - lookAheadDist, player.gameObject.transform.position.y, transform.position.z), fractionOfJourney);
            }
            else if (cameraRight && transform.position.x < player.gameObject.transform.position.x + lookAheadDist)
            {
                float distCovered = (Time.time - startTime) * lerpTime;
                float fractionOfJourney = distCovered / xDistToTravel;
                transform.position = Vector3.Lerp(transform.position, new Vector3(player.gameObject.transform.position.x + lookAheadDist, player.gameObject.transform.position.y, transform.position.z), fractionOfJourney);
            }
            if (cameraUp && transform.position.y < player.gameObject.transform.position.y + lookAheadDist)
            {
                float distCovered = (Time.time - startTime) * lerpTime;
                float fractionOfJourney = distCovered / yDistToTravel;
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.gameObject.transform.position.y + lookAheadDist, transform.position.z), fractionOfJourney);
            }
            else if (cameraDown && transform.position.y > player.gameObject.transform.position.y - lookAheadDist)
            {
                float distCovered = (Time.time - startTime) * lerpTime;
                float fractionOfJourney = distCovered / yDistToTravel;
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.gameObject.transform.position.y - lookAheadDist, transform.position.z), fractionOfJourney);
            }
        }

        if (enteredBossZone && bossAlive)
        {
            freeMovement = false;
            transform.position = Vector3.MoveTowards(transform.position, bossCamPos, cameraSpeed);
        }
        if (!bossAlive)
        {
            freeMovement = true;
        }
    }
}
