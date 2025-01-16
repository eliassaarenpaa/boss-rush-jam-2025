using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileForwardMovement : ProjectileComponent
{
    private int ricochetAmountLeft;

    public override void Setup()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;

        ricochetAmountLeft = base.tarotDataObject.tarotBaseData.projectileRicochetAmount;
    }

    private void Update()
    {
        transform.position += transform.forward * base.tarotDataObject.tarotBaseData.projectileSpeed * Time.deltaTime;
    }
}
