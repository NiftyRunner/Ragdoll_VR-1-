using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReset : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject bot;

    public void ResetWeapon()
    {

        GameObject rg = GameObject.FindGameObjectWithTag("Weapons");
        if (rg != null)
        {
            Destroy(rg);
        }
        Instantiate(bot, spawnLocation.position, Quaternion.identity);

    }
}
