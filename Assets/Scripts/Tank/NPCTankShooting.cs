using UnityEngine;
using UnityEngine.UI;

    public class NPCTankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 3;
        public Rigidbody m_Shell; 
        public Transform m_FireTransform;
        public Slider m_AimSlider;
        public AudioSource m_ShootingAudio;
        public AudioClip m_ChargingClip; 
        public AudioClip m_FireClip;
        public float m_MinLaunchForce = 1.0f;
        public float m_MaxLaunchForce = 100.0f;
        public float m_MaxChargeTime = 1.5f;
        public bool cargando;
        public bool dispara;
        public float distance;
        public float m_CurrentLaunchForce;
        public bool m_Fired;

        private string m_FireButton;        
        private float m_ChargeSpeed;
                             

        GameObject Slider;
        Slider s;

        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }

        private void Start()
        {
            // The fire axis is based on the player number.
            //    m_FireButton = "Fire" + m_PlayerNumber;

            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

        }

        private void Update()
        {

            // The slider should have a default value of the minimum launch force.
            m_AimSlider.value = m_MinLaunchForce;

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_Fired && dispara)
            {

                // ... reset the fired flag and reset the launch force.
                m_Fired = false; cargando = true;
                m_CurrentLaunchForce = m_MinLaunchForce;

                // Change the clip to the charging clip and start it playing.
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();

            }
            else if (cargando && !m_Fired)
            {

                // Increment the launch force and update the slider.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                // m_AimSlider.value = m_CurrentLaunchForce;
                m_AimSlider.value = distance;
                if (m_CurrentLaunchForce >= m_MaxLaunchForce)
                {
                    cargando = false;
                }
            }
            else if (!cargando && !m_Fired)
            {
                //con la distancia no se falla
                m_CurrentLaunchForce = distance;
                Fire();
            }

        }

        public void Fire()
        {
            // Set the fired flag so only Fire is only called once.
            m_Fired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;

        }
    }