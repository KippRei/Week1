using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float swingDegrees = 45f;
    [SerializeField] private float swingSpeed = 1f;
    [SerializeField] private GameObject axe;
    private float swingStartPoint;
    private float swingEndPoint;
    private GameObject newAxe;
    private bool axeActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwingAxe(Vector3 position, Quaternion rotation)
    {
        if (!axeActive)
        {
            axeActive = true;
            swingStartPoint = rotation.eulerAngles.z + (swingDegrees / 2);
            swingEndPoint = rotation.eulerAngles.z - (swingDegrees / 2);
            newAxe = Instantiate(axe, position, rotation);
            newAxe.transform.parent = transform;
            StartCoroutine(AxeSwing(rotation));
        }
    }

    IEnumerator AxeSwing(Quaternion rotation)
    {
        float swing = swingStartPoint;
        while (swing >= swingEndPoint)
        {
            swing -= swingSpeed;
            transform.localEulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, swing);
            yield return new WaitForEndOfFrameUnit();
        }
        axeActive = false;
        Destroy(newAxe);
    }
}
