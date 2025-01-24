using System;
using UnityEngine;

namespace Sandboxes.Cossu.CombatDemo
{
    public class ProjectileEvents : ProjectileRequiredComponent
    {
        public delegate void ProjectileTriggerEnter(Collider collider);
        public ProjectileTriggerEnter projectileTriggerEnter;

        public delegate void ProjectileTriggerStay(Collider collider);
        public ProjectileTriggerStay projectileTriggerStay;

        public delegate void ProjectileTriggerExit(Collider collider);
        public ProjectileTriggerExit projectileTriggerExit;

        public delegate void ProjectileCollisionEnter(Collision collision);
        public ProjectileCollisionEnter projectileCollisionEnter;

        public delegate void ProjectileCollisionStay(Collision collision);
        public ProjectileCollisionStay projectileCollisionStay;

        public delegate void ProjectileCollisionExit(Collision collision);
        public ProjectileCollisionExit projectileCollisionExit;
    }
}