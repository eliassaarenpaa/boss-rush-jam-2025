using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class MeatballExplodeOnDeath : MonoBehaviour
    {
        [SerializeField] private Transform sphere;
        [SerializeField] private GameObject explosionPrefab;

        public void Explode()
        {
            var explosion=  Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            var meatballExplosion = explosion.GetComponent<MeatballExplosion>();
            meatballExplosion.ScaleExplosion(sphere.localScale.x);
        }
    }
}
