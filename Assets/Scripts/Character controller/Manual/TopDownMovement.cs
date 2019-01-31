using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TopDownMovement : MonoBehaviour {


    private float h;                                      // Horizontal Axis.
    private float v;                                      // Vertical Axis.

    float speed = 1.5f;
    Animator animator;

    Rigidbody personRigidbody;

    Transform SpineBone;
    Transform RootBone;


    Quaternion spineOrientation;
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

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        personRigidbody = GetComponent<Rigidbody>();

        cam = Camera.main.transform;
        //  cam = Camera.main.transform.GetComponent<ThirdPersonOrbitCamBasic>();
        // Set the input axes on the Animator Controller.
        //  anim.SetFloat(hFloat, h, 0.1f, Time.deltaTime);
        // anim.SetFloat(vFloat, v, 0.1f, Time.deltaTime);

        var skeletHolder = GetComponentInChildren<SkeletHolder>();

        SpineBone = skeletHolder.m_SpineTransform;
        RootBone = skeletHolder.m_RootTransform;

        spineOrientation = SpineBone.transform.localRotation;
        shootDirection = Vector3.forward;
    }
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        aim = Input.GetMouseButton(1);

        if (aim)
        { Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                lookPos = hit.point;
            }

            lookDir = lookPos - transform.position;
            lookDir.y = 0;

            //

            animator.SetBool("Aim", aim);
        }
        else
        {
            animator.SetBool("Aim", aim);
           // 
        }
        Movement();
        Debug.Log("RootBone.localRotation " + RootBone.rotation);
    }

    private void LateUpdate()
    {
        
        Turn();

    }


    void Turn()
    {
        if (aim)
        {
            var angle = Vector3.Dot(RootBone.forward, lookDir.normalized);
            var angleWorld = Vector3.SignedAngle(Vector3.forward, lookDir.normalized, Vector3.up);
            Debug.Log("angle " + angle + " angleWorld " + angleWorld);           

           // Debug.Log("RootBone.localRotation " + RootBone.rotation);
            Debug.Log("SpineBone.forward " + SpineBone.forward);
            Debug.DrawLine(transform.position, transform.position + lookDir);

            if (angle > 0.72f)
            {
                SpineBone.localRotation = Quaternion.LookRotation(RootBone.InverseTransformDirection(lookDir), Vector3.up) * spineOrientation;
                //RootBone.localRotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                //  RootBone.localRotation = Quaternion.Euler(0, 0, 0);
                // SpineBone.localRotation = Quaternion.LookRotation(transform.InverseTransformDirection(shootDirection), Vector3.up) * spineOrientation;

                if (angleWorld < 22.5 && angleWorld > -22.5f)
                {
                    // forwardAmount = 1;
                    //strafeAmount = 0;
                    turnAmount = 0;
                }
                else if (angleWorld > 22.5 && angleWorld < 67.5f)
                {
                    //forwardAmount = 1;
                    //strafeAmount = 1;
                    turnAmount = 45;
                }

                else if (angleWorld > 67.5f && angleWorld < 112.5f)
                {
                    // forwardAmount = 0;
                    // strafeAmount = 1;
                    turnAmount = 90;
                }

                else if (angleWorld > 112.5f && angleWorld < 157.5f)
                {
                    // forwardAmount = 0;
                    // strafeAmount = 1;
                    turnAmount = 135;
                }

                else if (angleWorld > 157.5f || angleWorld < -157.5f)
                {
                    // forwardAmount = -1;
                    // strafeAmount = 0;
                    turnAmount = 180;
                }

                else if (angleWorld < -112.5f && angleWorld > -157.5f)
                {
                    // forwardAmount = -1;
                    // strafeAmount = -1;
                    turnAmount = 215;
                }
                else if (angleWorld < -67.5f && angleWorld > -112.5f)
                {
                    //  forwardAmount = 0;
                    //  strafeAmount = -1;
                    turnAmount = 270;
                }
                else if (angleWorld < -22.5 && angleWorld > -67.5f)
                {
                    // forwardAmount = -1;
                    // strafeAmount = -1;
                    turnAmount = 305;
                }
            }

            RootBone.localRotation = Quaternion.Euler(0, turnAmount, 0);

        }
        else
        {
            RootBone.LookAt(transform.position + new Vector3(h, 0, v), Vector3.up);

        }
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


        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Vector3 localMove = RootBone.InverseTransformDirection(move);

        strafeAmount = localMove.x;
        forwardAmount = localMove.z;

        personRigidbody.velocity = move * speed;

        

        

        animator.SetFloat("V", forwardAmount);
        animator.SetFloat("H", strafeAmount);
        animator.SetFloat("Speed", move.magnitude, 0.1f, Time.deltaTime);

        // animator.SetFloat("Speed", move.magnitude);
        // Vector3 camVerctor3 = Quaternion.Euler(0, cam.GetH, 0).eulerAngles;
        // Vector3 camVerctor = new Vector3(Mathf.Sin(cam.GetH * Mathf.PI / 180 ), 0,  Mathf.Cos(cam.GetH * Mathf.PI / 180));

        //   float cosa = Mathf.Cos(cam.GetH * Mathf.PI / 180);
        //  float sina = Mathf.Sin(cam.GetH * Mathf.PI / 180);

        //  Debug.Log("Quaternion " + camVerctor3+ " camVerctor " + camVerctor + " hv " + new Vector3(h, 0, v).normalized);     


        //if (vec.magnitude > 0)
        //    personRigidbody.rotation  = Quaternion.LookRotation (vec);

    }
}
