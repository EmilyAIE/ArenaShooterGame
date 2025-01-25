using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Player Details")]
    [SerializeField] private float m_maxHealth;
    private float m_currentHealth;

    //private HUDManager m_hudManager;

    public bool isAlive;

    private void Start()
    {
        isAlive = true;
        m_currentHealth = m_maxHealth;
        //m_hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        //m_hudManager.AdjustHealthText(m_currentHealth);
        //m_hudManager.AdjustHealthAnimationValue(m_currentHealth);
    }

    public void TakeDamage(float value)
    {
        m_currentHealth -= value;

        if (m_currentHealth < 0)
            m_currentHealth = 0;

        //ui
        //m_hudManager.AdjustHealthText(m_currentHealth);

        //lady
        //m_hudManager.AdjustHealthAnimationValue(m_currentHealth);
        //m_hudManager.onHit_LadyAnimationTrigger();
        if (m_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().OnDeath();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public void GainHealth(float value)
    {
        m_currentHealth += value;
        if (m_currentHealth > m_maxHealth)
            m_currentHealth = m_maxHealth;

        //ui
        //m_hudManager.AdjustHealthText(m_currentHealth);

        //lady
        //m_hudManager.AdjustHealthAnimationValue(m_currentHealth);
    }
}
