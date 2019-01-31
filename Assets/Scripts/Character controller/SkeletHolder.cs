using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkeletHolder : MonoBehaviour {

    public Transform m_SpineTransform;
    public Transform m_RootTransform;

    public Transform m_RightHandSocket;
    public Transform m_LeftHandSocket;

    public UnityAction StartAttackEvent;

     public UnityAction EndAttackEvent;


    private void Start()
    {
        if (m_RightHandSocket != null)

        {
            float scaleX = m_RightHandSocket.localScale.x / m_RightHandSocket.parent.localScale.x;
            float scaleY = m_RightHandSocket.localScale.y / m_RightHandSocket.parent.localScale.y;
            float scaleZ = m_RightHandSocket.localScale.z / m_RightHandSocket.parent.localScale.z;

            m_RightHandSocket.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }

        if (m_LeftHandSocket != null)

        {
            float scaleX = m_LeftHandSocket.localScale.x / m_LeftHandSocket.parent.localScale.x;
            float scaleY = m_LeftHandSocket.localScale.y / m_LeftHandSocket.parent.localScale.y;
            float scaleZ = m_LeftHandSocket.localScale.z / m_LeftHandSocket.parent.localScale.z;

            m_LeftHandSocket.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
    void StartAttack()
    {
        if (StartAttackEvent != null)
            StartAttackEvent();
    }

    void EndAttack()
    {
        if (EndAttackEvent != null)
            EndAttackEvent();
    }
}
