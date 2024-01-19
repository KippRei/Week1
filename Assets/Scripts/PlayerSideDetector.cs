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

    private void OnTrigger2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "doNotPass")
        {
            Debug.Log(col.gameObject.tag);
            contact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        contact = false;
    }
}
