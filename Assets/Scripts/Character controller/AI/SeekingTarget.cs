using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeekingTarget : MonoBehaviour {
    [SerializeField]
    float seekRadius;
    List<Collider> targets;
    List<GameObject> damagableTargets;

    public GameObject currentTarget;
    public UnityAction SeedTarget;

    // Use this for initialization
    void Start () {
        targets = new List<Collider>();
        damagableTargets = new List<GameObject>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(targets.Count == 0)
            targets.AddRange( Physics.OverlapSphere(transform.position, seekRadius, LayerMask.GetMask("Town")));

        if(targets.Count != 0 && currentTarget == null)
        {
            foreach(Collider target in targets )
            {

                IDamagable foe = target.GetComponent<IDamagable>();
                if (foe != null)
                    damagableTargets.Add(target.gameObject);
            }

            SelectionTarget();
        }

        

    }

    void SelectionTarget()
    {
        float minDist = seekRadius;
        foreach (GameObject target in damagableTargets)
        {
            var dist = (target.transform.position- transform.position).magnitude;
           if (dist > 0  && dist < minDist)
            {
                minDist = dist;
                currentTarget = target;

                Debug.Log(name + " dist " + dist + " target" + target.name);
            }
        }

        if(SeedTarget != null && currentTarget != null)
        {
            SeedTarget();
        }
    }
}
