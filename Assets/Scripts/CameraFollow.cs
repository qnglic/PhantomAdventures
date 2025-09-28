using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);
    [SerializeField] private float smoothing = 1.0f;
    private PlayerMovement targetScript;
    private Vector3 temp;


    // Update is called once per frame
    void LateUpdate()
    {
        targetScript = target.GetComponent<PlayerMovement>();
        temp = offset;

        if (targetScript.isFlipped)
        {
            temp.y *= -1;
        }

        //print(temp.x);
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + temp, smoothing * Time.deltaTime);
        transform.position = newPosition;
    }
    

}
