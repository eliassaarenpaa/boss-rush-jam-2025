using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DamageTrigger : MonoBehaviour
{
    [SerializeField] float damageAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.ChangeCurrentHealth(damageAmount, Health.CurrentHealthChangeType.Damage);
        }
    }
}
