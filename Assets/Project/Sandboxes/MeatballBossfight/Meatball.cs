using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    [RequireComponent(typeof(MeatballSplitter))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Damageable))]
    public class Meatball : MonoBehaviour
    {
        [HideInInspector] public MeatballSplitter splitter;
        [HideInInspector] public Damageable damageable;
        [HideInInspector] public Rigidbody rb;
        public GameObject model;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            damageable = GetComponent<Damageable>();
            splitter = GetComponent<MeatballSplitter>();
        }
    }
}
