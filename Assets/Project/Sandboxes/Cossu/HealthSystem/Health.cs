using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEditor.Graphs;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Events
    public delegate void OnKill();
    public OnKill onKill;
    #endregion

    //Health values
    [Header("Health Objects")]
    [SerializeField] private ScriptableFloat currentHealth;
    [SerializeField] private ScriptableFloat maxHealth;

    [Header("Settings")]
    [SerializeField] private bool destroyOnKill;

    private void Awake()
    {
        ChangeCurrentHealth(maxHealth.Value, ScriptableFloat.FloatChangeType.Set);
    }

    public void ChangeCurrentHealth(float value, ScriptableFloat.FloatChangeType type)
    {
        currentHealth.ModifyFloat(Mathf.Abs(value), type);

        currentHealth.ModifyFloat(Mathf.Clamp(currentHealth.Value, 0, maxHealth.Value), ScriptableFloat.FloatChangeType.Set); //Clamp the health so it doesn't go over max
        if (currentHealth.Value <= 0) Kill(); //If zero -> Kill
    }

    public void ChangeMaxHealth(float value, ScriptableFloat.FloatChangeType type)
    {
        maxHealth.ModifyFloat(Mathf.Abs(value), type);
        maxHealth.ModifyFloat(Mathf.Clamp(maxHealth.Value, 0, Mathf.Infinity), ScriptableFloat.FloatChangeType.Set); //Clamp so it does not go below 0
        if(currentHealth.Value > maxHealth.Value)
        {
            currentHealth.ModifyFloat(maxHealth.Value, ScriptableFloat.FloatChangeType.Set);
        }
        if (maxHealth.Value <= 0) Kill();
    }

    public void Kill()
    {
        onKill?.Invoke();
        if(destroyOnKill) Destroy(gameObject);
    }
}
