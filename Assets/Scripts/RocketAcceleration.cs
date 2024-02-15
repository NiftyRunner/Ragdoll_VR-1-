using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAcceleration : MonoBehaviour
{
    [SerializeField] Rigidbody rocketBody;
    [SerializeField] float launchForce = 10f;

    void Start()
    {
        print("AAAA");
        rocketBody = GetComponent<Rigidbody>();
        rocketBody.AddForce(transform.forward * launchForce * Time.deltaTime, ForceMode.Impulse);
        Debug.Log("Force Added");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
