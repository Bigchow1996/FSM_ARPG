using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    public Transform targetTF;
    private Vector3 relativePosition;

    private void Start()
    {
        relativePosition = transform.position - targetTF.position;
    }
    private void Update()
    {
        transform.position = targetTF.position + relativePosition;
    }
}
