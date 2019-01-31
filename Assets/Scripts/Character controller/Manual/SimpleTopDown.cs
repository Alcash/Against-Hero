using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(PlayerCombatController), typeof(CharacterCore))]
public class SimpleTopDown : MonoBehaviour {



    private float h;                                      // Horizontal Axis.
    private float v;                                      // Vertical Axis.

    
 

    Animator animator;

    Rigidbody personRigidbody;
    CharacterCore core;
    PlayerCombatController combatController;
    Vector3 shootDirection;
    //ThirdPersonOrbitCamBasic cam;
    Vector3 lookDir;
    Vector3 lookPos;
    int turnAmount = 0;

    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float strafeAmount;

    bool aim;
    bool attack;



    // Use this for initialization
    void Start () {

        core = GetComponent<CharacterCore>();
        animator = GetComponentInChildren<Animator>();
        personRigidbody = GetComponent<Rigidbody>();
        combatController = GetComponent<PlayerCombatController>();

        cam = Camera.main.transform;
        lookDir = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        aim = Input.GetMouseButton(1);

        attack = Input.GetMouseButton(0);

    }

   

    private void FixedUpdate()
    {
       
        Movement();
        Turn();

        if(attack)
            combatController.Attack();
    }

    void Turn()
    {

        if (aim)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                lookPos = hit.point;
            }

            lookDir = lookPos - transform.position;
            lookDir.y = 0;

            Debug.DrawLine(transform.position, transform.position + lookDir);
            transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);           
        }
        else
        {
            
            transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);            
        }

        animator.SetBool("Aim", aim);
    }

    void Movement()
    {

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = v * camForward + h * cam.right;
        }
        else
        {
            move = v * Vector3.forward + h * Vector3.right;
        }

        if(move.magnitude >0)
        {
            lookDir = move;
        }

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Vector3 localMove = transform.InverseTransformDirection(move);

        strafeAmount = localMove.x;
        forwardAmount = localMove.z;

        personRigidbody.velocity = move * core.movementSpeed;

        animator.SetFloat("V", forwardAmount);
        animator.SetFloat("H", strafeAmount);
        animator.SetFloat("Speed", move.magnitude, 0.1f, Time.deltaTime);       

    }

    
}
