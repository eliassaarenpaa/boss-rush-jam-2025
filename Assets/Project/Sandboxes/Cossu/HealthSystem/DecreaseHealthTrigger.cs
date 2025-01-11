using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DecreaseHealthTrigger : MonoBehaviour
{
    [SerializeField] float decreaseAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.DecreaseHealth(decreaseAmount);
        }
    }
}
