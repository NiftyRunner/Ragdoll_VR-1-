using DynamicRagdoll;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] float explosionForce = 100f;
        [SerializeField] float explosionRadius = 15f;

        RagdollController rdController;

        Collider[] ragdollCollider = new Collider[20];

        // --- Config ---
        public float speed = 100;
        public LayerMask collisionLayerMask;

        // --- Explosion VFX ---
        public GameObject rocketExplosion;

        // --- Projectile Mesh ---
        public MeshRenderer projectileMesh;

        // --- Script Variables ---
        private bool targetHit;

        // --- Audio ---
        public AudioSource inFlightAudioSource;

        // --- VFX ---
        public ParticleSystem disableOnHit;

        List<GameObject> blastableGameObjects = new List<GameObject>();

        private void Start()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Explodable");
            foreach (GameObject ob in objects)
                blastableGameObjects.Add(ob);
        }

        private void Update()
        {
            // --- Check to see if the target has been hit. We don't want to update the position if the target was hit ---
            if (targetHit) return;

            // --- moves the game object in the forward direction at the defined speed ---
            transform.position += transform.forward * (speed * Time.deltaTime);
        }


        /// <summary>
        /// Explodes on contact.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // --- return if not enabled because OnCollision is still called if compoenent is disabled ---
            if (!enabled) return;

            // --- Explode when hitting an object and disable the projectile mesh ---
            Explode();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach(Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();

            if(collision.gameObject.tag == "Explodable")
            {
                rdController = collision.gameObject.GetComponent<RagdollController>();
                if(rdController != null)
                { 
                    rdController.GoRagdoll("from collision");
                    Explode();
                }
            }


            // --- Destroy this object after 2 seconds. Using a delay because the particle system needs to finish ---
            Destroy(gameObject, 5f);
        }


        /// <summary>
        /// Instantiates an explode object.
        /// </summary>
        private void Explode()
        {
            Debug.LogWarning("Explode");
            ExplosionForce();
            // --- Instantiate new explosion option. I would recommend using an object pool ---
            GameObject newExplosion = Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);

        }

        public void ExplosionForce()
        {
            //GameObject targetCube = GameObject.FindGameObjectWithTag("TargetCube");
            //Debug.LogWarning(transform.position + ", Box: " + targetCube.transform.position +
            //                ", Dist:" + Vector3.Distance(transform.position, targetCube.transform.position));


            foreach (GameObject ob in blastableGameObjects)
            {
                if (Vector3.Distance(transform.position, ob.transform.position) < explosionRadius)
                {
                    Explode(ob);
                }
            }

            //print("Exploding");
            ////Debug.LogWarning(ragdollCollider[i].name + "doesnt have rb");
            //int numCollider = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, ragdollCollider);
            //print(numCollider);
            //if(numCollider > 0)
            //{
            //    for (int i = 0; i < numCollider; i++)
            //    {
            //        if (ragdollCollider[i].name == "TargetCube")
            //            Debug.LogWarning("HIT");
            //        //if (ragdollCollider[i].TryGetComponent(out Rigidbody rb))
            //        //{
            //        //    Debug.LogWarning(rb.name);
            //        //    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);    
            //        //} else
            //        //{
            //        //    Debug.LogWarning(ragdollCollider[i].name + "doesnt have rb");
            //        //}
            //    }
            //}


        }

        void Explode(GameObject ob)
        {
            if (ob.TryGetComponent(out Rigidbody _rb))
            {
                Debug.LogWarning("Force added on " +  ob.name);
                _rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            else
                Debug.LogWarning("RB not in " + ob.name);
        }
    }
}