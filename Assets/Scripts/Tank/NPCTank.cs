using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTank : MonoBehaviour
{

    public LayerMask m_TankMask;
    public float m_SphereRadius = 50f;
    public float m_Speed = 6f;
    public float m_TurnSpeed = 90f;

    NavMeshAgent nav;
    NPCTankShooting shoot;
    private float shootDistance = 20;
    private GameObject m_NearTank;
    private Vector3 m_CompareDistance;
    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        shoot = GetComponent<NPCTankShooting>();

    }

    GameObject FindClosestTank()
    {
        GameObject[] tanques;
        tanques = GameObject.FindGameObjectsWithTag("playerTank");
        float distance = Mathf.Infinity;

        for (int i = 0; i < tanques.Length; i++)
        {
            m_CompareDistance = tanques[i].transform.position - this.transform.position;
            float m_SqrDistance = m_CompareDistance.sqrMagnitude;

            if (m_SqrDistance < distance)
            {
                m_NearTank = tanques[i];
                distance = m_SqrDistance;
            }
        }
        return m_NearTank;
    }

    void FixedUpdate()
    {
        GameObject closestTank = FindClosestTank();
        nav.stoppingDistance = shootDistance;

        if(nav.remainingDistance <= shootDistance)
        {
            Vector3 direccion = (closestTank.transform.position - transform.position);
            Quaternion miDireccion = Quaternion.LookRotation(direccion.normalized);
            Quaternion rotacion = Quaternion.RotateTowards(transform.rotation, miDireccion, 1.0f);

            if(Quaternion.Angle(rotacion,miDireccion) < 2.0)
            {
                shoot.dispara = true;
                shoot.distance = direccion.magnitude;
            }
            else
            {
                shoot.dispara = false;
            }
            nav.SetDestination(closestTank.transform.position);
            m_Rigidbody.MoveRotation(rotacion);
            
        }
        




        //transform.position = Vector3.MoveTowards(transform.position, closestTank.transform.position, nav.speed * Time.fixedDeltaTime);
    }
}
