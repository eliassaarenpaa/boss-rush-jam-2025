using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight
{
    public class MeatballExplosion : MonoBehaviour
    {
        [SerializeField] private float scaleUpSpeed;
        private Vector3 _scale;

        private void Awake()
        {
            _scale = Vector3.zero;
        }

        private void FixedUpdate()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _scale, Time.deltaTime * scaleUpSpeed);
            
            if( Vector3.Distance(transform.localScale, _scale) < 0.01f)
            {
                Destroy(gameObject);
            }
        }

        public void ScaleExplosion(float scale)
        {
            _scale = Vector3.one * scale * 1.25f;
        }
    }
}
