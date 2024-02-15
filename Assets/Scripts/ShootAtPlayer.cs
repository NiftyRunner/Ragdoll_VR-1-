using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    GameObject player;
    
    [SerializeField] FireBulletOnActive weapon;
    [SerializeField] int waitTime = 7;

    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
        StartCoroutine(FireDelay());
    }


    void Update()
    {
        transform.LookAt(player.transform.position);  
    }

    IEnumerator FireDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
}
