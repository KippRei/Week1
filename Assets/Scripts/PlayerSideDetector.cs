using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideDetector : MonoBehaviour
{
    public bool contact = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "doNotPass")
        {
            contact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        contact = false;
    }
}
