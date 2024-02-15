using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject bot;

    string destroyName;

    private void Start()
    {
        destroyName = bot.name;
    }

    public void Spawner()
    { 
            Instantiate(bot, spawnLocation.position, Quaternion.identity);    
    }

    public void ResetRange()
    {

        GameObject rg = GameObject.FindGameObjectWithTag("Range");
        if (rg != null) 
        {
            Destroy(rg);
        }
        Instantiate(bot, spawnLocation.position, Quaternion.identity);

    }
}
