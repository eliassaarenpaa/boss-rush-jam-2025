using System;
using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight.Animations
{
    public class DestroyGameObjectAfterDelay : MonoBehaviour
    {
        [SerializeField] private float delay = 0.0f;

        private void OnEnable()
        {
            Destroy(gameObject, delay);
        }
    }
}
