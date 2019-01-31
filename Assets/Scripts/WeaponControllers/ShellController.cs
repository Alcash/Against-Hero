using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShellController : MonoBehaviour {

    public UnityAction<GameObject> HitColliderEvent;
    Rigidbody shellRigid;

    private void Start()
    {
        shellRigid.velocity = Vector3.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(HitColliderEvent != null)
        {
            HitColliderEvent(collision.gameObject);
        }
    }

}
