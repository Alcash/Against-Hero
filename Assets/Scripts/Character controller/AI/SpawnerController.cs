using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinionManager))]
public class SpawnerController : MonoBehaviour {

    public float minionSpawnSpeed = 3;
    float minionSpawnCooldown = 0;
    
    GameObject[] minionsPool;  

    MinionManager minionManager;

    // Use this for initialization
    void Start () {

        minionsPool = Resources.LoadAll<GameObject>("Minions");
        minionManager = GetComponent<MinionManager>();

    }


    private void FixedUpdate()
    {


        if (minionSpawnCooldown > 0)
        {
            minionSpawnCooldown -= Time.fixedDeltaTime;
        }
        else
        {
            minionSpawnCooldown = 1 / minionSpawnSpeed;
            float x = UnityEngine.Random.Range(-10, 10);

            Vector3 pos = transform.position;
            pos.x = x;
            Spawn(pos);

        }
    }

    internal void Spawn(Vector3 point)
    {

        int indexMinion = UnityEngine.Random.Range( 0, minionsPool.Length);

        GameObject minion_go = Instantiate(minionsPool[indexMinion], point, Quaternion.Euler(Vector3.back));

        var minion = minion_go.GetComponent<MinionController>();

        minionManager.AddMinion(minion);

      
        
    }
}
