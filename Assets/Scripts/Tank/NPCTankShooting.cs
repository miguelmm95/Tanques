using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTankShooting : MonoBehaviour
{
    public bool m_Fire;
    public float m_Force = 20;

    public Rigidbody m_Shell;
    public float m_Distancia;
    public Transform m_FireTransform;
    /*public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;*/


    public void Fire()
    {
        m_Fire = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_Distancia, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_Force * m_FireTransform.forward;

        /*m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();*/
    }
}
