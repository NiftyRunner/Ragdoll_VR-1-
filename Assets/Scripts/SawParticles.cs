using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem sawParticles;
    [SerializeField] float waitTime = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explodable")
        {
            GameObject instance = Instantiate(sawParticles.gameObject, collision.gameObject.transform.position, Quaternion.identity);

            StartCoroutine(DestroyClone(instance));
        }
    }

    IEnumerator DestroyClone(GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(objectToDestroy);
        Debug.Log("Collided");
    }
}
