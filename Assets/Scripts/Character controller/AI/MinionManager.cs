using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour {

    public Transform goalTransform;

    List<MinionController> currentMinions;

    // Use this for initialization
    void Start () {
        currentMinions = new List<MinionController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void AddMinion(MinionController minion)
    {
        currentMinions.Add(minion);
        minion.SetGoal(goalTransform);
    }
}
