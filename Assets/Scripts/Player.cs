using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    float movementSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //PlayerRotate();
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 camPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 playerPosition = gameObject.transform.position;

        if (Input.GetButton("left") && camPos.x > 0)
        {
            playerPosition.x -= movementSpeed;
            gameObject.transform.position = playerPosition;
        }
        else if (Input.GetButton("right") && camPos.x < 1)
        {
            playerPosition.x += movementSpeed;
            gameObject.transform.position = playerPosition;
        }
        if (Input.GetButton("up") && camPos.y < 1)
        {
            playerPosition.y += movementSpeed;
            gameObject.transform.position = playerPosition;
        }
        else if (Input.GetButton("down") && camPos.y > 0)
        {
            playerPosition.y -= movementSpeed;
            gameObject.transform.position = playerPosition;
        }
    }

    void PlayerRotate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = gameObject.transform.position;
        float yCoord = mousePosition.y - playerPosition.y;
        float xCoord = mousePosition.x - playerPosition.x;
        float rotateToRad = Mathf.Atan(yCoord / xCoord);
        float toAdd = -90;
        if (mousePosition.x < playerPosition.x)
        {
            toAdd = 90;
        }
        float rotateToDeg = rotateToRad * (180 / Mathf.PI) + toAdd;
    /*    Debug.Log("Mouse Position x = " + mousePosition.x);
        Debug.Log("Mouse Position y = " + mousePosition.y);
        Debug.Log("Player Position x = " + playerPosition.x);
        Debug.Log("Player Position y = " + playerPosition.y);
        Debug.Log("Angle in Rads = " + rotateToRad);
        Debug.Log("Angle in Degrees = " + rotateToDeg);*/
        transform.rotation = Quaternion.Euler(0, 0, rotateToDeg);
    }
}