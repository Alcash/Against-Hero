using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterCore),typeof(SeekingTarget))]
public class MinionController : MonoBehaviour {


    //MinionCombatController combatcontroller;
    CharacterCore core;
    SeekingTarget seeker;
    NavMeshAgent agent;

    Transform target;

    Transform goalTarget;

    float timeToDecideBehavior = 2;
    float time;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        seeker = GetComponent<SeekingTarget>();
        core = GetComponent<CharacterCore>();
        agent.speed = core.movementSpeed;

        seeker.SeedTarget += NewTarget;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (time > 0)
        {
            time -= Time.fixedDeltaTime;

        }
        else
        {
            time = timeToDecideBehavior;
            SelectBehaviors();
        }

    }

    void SelectBehaviors()
    {
        agent.SetDestination(target.position);
    }

    void SelectTarget()
    {
        target = goalTarget;
    }

    internal void SetGoal(Transform goalTransform)
    {
        goalTarget = goalTransform;

        SelectTarget();
    }

    void NewTarget()
    {

        Ray ray = new Ray(transform.position, seeker.currentTarget.transform.position - transform.position);

        target = seeker.currentTarget.transform;
    }
}
