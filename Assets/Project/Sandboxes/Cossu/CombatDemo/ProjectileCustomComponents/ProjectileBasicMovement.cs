using UnityEngine;

public class ProjectileBasicMovement : CustomProjectileComponent
{
    public override void Start()
    {
        ProjectileEvents projectileEvents = container.GetSerializableComponent<ProjectileEvents>();
    }

    public override void Update()
    {
        transform.position += transform.forward * tarotData.tarotBaseData.projectileSpeed * Time.deltaTime;
    }
}