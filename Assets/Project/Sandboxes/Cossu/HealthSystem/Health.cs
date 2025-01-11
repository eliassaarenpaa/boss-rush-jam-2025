using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEditor.Graphs;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Events
    public delegate void OnKillEvent();
    public OnKillEvent onKill;

    public delegate void OnDamageTakenEvent(float amount);
    public OnDamageTakenEvent onDamageTaken;

    public delegate void OnHealedEvent(float amount);
    public OnHealedEvent onHealed;

    public delegate void OnCurrentHealthDecreasedEvent(float amount);
    public OnCurrentHealthDecreasedEvent onCurrentHealthDecreased;

    public delegate void OnCurrentHealthAddedEvent(float amount);
    public OnCurrentHealthAddedEvent onCurrentHealthAdded;

    public delegate void OnCurrentHealthSetEvent(float newCurrentHealth);
    public OnCurrentHealthSetEvent onCurrentHealthSet;

    public delegate void OnMaxHealthDecreasedEvent(float amount);
    public OnMaxHealthDecreasedEvent onMaxHealthDecreased;

    public delegate void OnMaxHealthAddedEvent(float amount);
    public OnMaxHealthAddedEvent onMaxHealthAdded;

    public delegate void OnMaxHealthSetEvent(float newMaxHealth);
    public OnMaxHealthSetEvent onMaxHealthSet;
    #endregion

    //Health values
    [SerializeField] [ReadOnly] private float currentHealth;
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    #region Current Health Functions
    public float GetCurrentHealth() { return currentHealth; }

    public void AddHealth(float amount)
    {
        currentHealth += Mathf.Abs(amount);
        onCurrentHealthAdded?.Invoke(amount);
        if (currentHealth >= maxHealth) currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= Mathf.Abs(amount);
        onCurrentHealthDecreased?.Invoke(amount);
        if (currentHealth <= 0)
        {
            Kill();
            return;
        }
    }

    public void SetHealth(float newHealth)
    {
        if (newHealth <= 0)
        {
            Kill();
            return;
        }
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        onCurrentHealthSet?.Invoke(currentHealth);
    }
    #endregion

    #region Max Health Modify Functions
    public float MaxHealth() { return maxHealth; }

    public void AddMaxHealth(float amount)
    {
        maxHealth += Mathf.Abs(amount);
        onMaxHealthAdded?.Invoke(amount);
    }

    public void DecreaseMaxHealth(float amount)
    {
        maxHealth -= Mathf.Abs(amount);
        onMaxHealthDecreased?.Invoke(amount);
        if(maxHealth <= 0)
        {
            Kill();
            return;
        }
        if(currentHealth >= maxHealth) currentHealth = maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        if(newMaxHealth <= 0)
        {
            Kill();
            return;
        }
        maxHealth = newMaxHealth;
        if (currentHealth >= maxHealth) currentHealth = maxHealth;
    }
    #endregion

    #region Combat functions
    public void Damage(float amount)
    {
        DecreaseHealth(amount);
        onDamageTaken?.Invoke(amount);
    }

    public void Heal(float amount)
    {
        AddHealth(amount);
        onHealed?.Invoke(amount);
    }

    public void Kill()
    {
        onKill.Invoke();
    }
    #endregion
}
