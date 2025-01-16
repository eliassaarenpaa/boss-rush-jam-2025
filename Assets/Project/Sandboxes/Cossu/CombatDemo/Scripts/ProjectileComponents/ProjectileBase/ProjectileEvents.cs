using UnityEngine;

public class ProjectileEvents : MonoBehaviour
{
    public delegate void OnProjectileRicochet();
    public OnProjectileRicochet onProjectileRicochet;

    public delegate void OnProjectileCollisionEnter(Collision collision);
    public OnProjectileCollisionEnter onProjectileCollisionEnter;
}