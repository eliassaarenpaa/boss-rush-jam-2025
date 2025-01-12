using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEditor.Graphs;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Events
    public delegate void OnCurrentHealthChangedEvent(float newCurrentHealth, CurrentHealthChangeType typeOfChange);
    public OnCurrentHealthChangedEvent onCurrentHealthChanged;

    public delegate void OnMaxHealthChangedEvent(float newMaxHealth, MaxHealthChangeType typeOfChange);
    public OnMaxHealthChangedEvent onMaxHealthChanged;

    public delegate void OnKill();
    public OnKill onKill;
    #endregion

    //Health values
    [Header("Health Status")]
    [SerializeField] [ReadOnly] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Settings")]
    [SerializeField] private bool destroyOnKill;

    private void Awake()
    {
        ChangeCurrentHealth(maxHealth, CurrentHealthChangeType.Set);
    }

    public enum CurrentHealthChangeType { Add, Decrease, Set, Damage, Heal }
    public float GetCurrentHealth() { return currentHealth; }
    public void ChangeCurrentHealth(float value, CurrentHealthChangeType type)
    {
        switch (type)
        {
            case CurrentHealthChangeType.Add:
                currentHealth += Mathf.Abs(value);
                break;
            case CurrentHealthChangeType.Decrease:
                currentHealth -= Mathf.Abs(value);
                break;
            case CurrentHealthChangeType.Set:
                currentHealth = Mathf.Abs(value);
                break;
            case CurrentHealthChangeType.Damage:
                currentHealth -= Mathf.Abs(value);
                break;
            case CurrentHealthChangeType.Heal:
                currentHealth += Mathf.Abs(value);
                break;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //Clamp the health so it doesn't go over max
        if (currentHealth <= 0) Kill(); //If zero -> Kill

        onCurrentHealthChanged?.Invoke(currentHealth, type);
    }

    public enum MaxHealthChangeType { Add, Decrease, Set}
    public float GetMaxHealth() {  return maxHealth; }
    public void ChangeMaxHealth(float value, MaxHealthChangeType type)
    {
        switch(type)
        {
            case MaxHealthChangeType.Add:
                maxHealth += Mathf.Abs(value);
                break;
            case MaxHealthChangeType.Decrease:
                maxHealth -= Mathf.Abs(value);
                break;
            case MaxHealthChangeType.Set:
                maxHealth = Mathf.Abs(value);
                break;
        }
        maxHealth = Mathf.Clamp(maxHealth, 0, Mathf.Infinity); //Clamp so it does not go below 0
        if(currentHealth > maxHealth)
        {
            ChangeCurrentHealth(maxHealth, CurrentHealthChangeType.Set);
        }
        if (maxHealth <= 0) Kill();

        onMaxHealthChanged?.Invoke(maxHealth, type);
    }

    public void Kill()
    {
        onKill?.Invoke();
        if(destroyOnKill) Destroy(gameObject);
    }
}
