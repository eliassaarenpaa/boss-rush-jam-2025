using UnityEngine;

public class ProjectileForwardChangeRicochet : ProjectileComponent
{
    private int ricochetAmountLeft;

    public override void Setup()
    {
        base.projectileEvents.onProjectileCollisionEnter += OnProjectileCollisionEnter;
        ricochetAmountLeft = base.tarotDataObject.tarotBaseData.projectileRicochetAmount;
    }

    private void OnProjectileCollisionEnter(Collision col)
    {
        if (ricochetAmountLeft < 1)
        {
            Destroy(gameObject);
            return;
        }

        //We have collided. Lets ricochet.
        transform.forward = Vector3.Reflect(transform.forward, col.GetContact(0).normal);
        base.projectileEvents.onProjectileRicochet?.Invoke();
        ricochetAmountLeft -= 1;
    }
}
