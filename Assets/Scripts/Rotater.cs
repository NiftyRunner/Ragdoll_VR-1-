using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 100f;


    void Update()
    {
        Vector3 rotationToAdd = new Vector3(0, rotateSpeed * Time.deltaTime, 0);
        transform.Rotate(rotationToAdd);

    }
}
