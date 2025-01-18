using UnityEngine;

public class ProjectileBasicMovement : SerializableProjectileComponent
{
    public override void Update()
    {
        transform.position += transform.forward * tarotData.tarotBaseData.projectileSpeed * Time.deltaTime;
    }
}