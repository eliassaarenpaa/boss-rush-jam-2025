using System;
using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class MeatballExplodeOnDeath : MonoBehaviour
    {
        [SerializeField] private Damageable damageable;
        [SerializeField] private GameObject explosionPrefab;

        /// <summary>
        ///     Called by the animation event in the MeatballDeath animation.
        /// </summary>
        public void Explode()
        {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            var meatballExplosion = explosion.GetComponent<MeatballExplosion>();
            
            var health = damageable.Health;
            var scaleT = Mathf.InverseLerp(health.GetMinValue(), health.GetMaxValue(), health.Value);
            var scale = Mathf.Lerp(20, 50, scaleT);
            
            meatballExplosion.ScaleExplosion(scale);
        }
    }
}
