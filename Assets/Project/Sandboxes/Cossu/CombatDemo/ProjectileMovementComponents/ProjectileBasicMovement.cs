using UnityEngine;

public class ProjectileBasicMovement : ProjectileMovementComponent
{
    private float projectileSpeed;

    public override void Start()
    {
        ProjectileEvents projectileEvents = container.GetSerializableComponent<ProjectileEvents>();
        projectileSpeed = weaponStatContainer.StatQuery(StatType.ProjectileSpeed).TrueValue;
    }

    public override void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }
}