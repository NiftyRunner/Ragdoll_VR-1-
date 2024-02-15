using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] Transform spawnPoint;

    [SerializeField] Rigidbody rocketBody;
    [SerializeField] float launchForce = 100f;


    void Start()
    {
        XRGrabInteractable grabable = GetComponent<XRGrabInteractable>();
        grabable.activated.AddListener(FireRocket);
        rocketBody = GetComponent<Rigidbody>();
        Debug.Log("Force Added");
    }

    public void FireRocket(ActivateEventArgs arg0)
    {
        GameObject spawedRocket = Instantiate(rocket);
        spawedRocket.transform.position = spawnPoint.position;
        rocketBody.velocity = spawnPoint.forward * launchForce * Time.deltaTime;
    }

}
