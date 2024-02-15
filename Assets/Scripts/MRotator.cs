using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 100f;


    void Update()
    {
        Vector3 rotationToAdd = new Vector3(rotateSpeed * Time.deltaTime, 0, 0);
        transform.Rotate(rotationToAdd);

    }
}
