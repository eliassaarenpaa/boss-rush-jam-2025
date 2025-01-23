using DG.Tweening;
using UnityEngine;

namespace Project.Sandboxes.MeatballBossfight.Animations
{
    public class MeatballOnHitVFX : MonoBehaviour
    {
        [SerializeField] private Transform scaleReference;
        [SerializeField] private float punchDuration = 0.5f;
        [SerializeField] private float punchStrength = 0.5f;
        [SerializeField] private int punchVibrato = 10;
        [SerializeField] private int punchElasticity = 1;
        [SerializeField] private Ease punchEase = Ease.OutBounce;
        [SerializeField] private Damageable damageable;
        [SerializeField] private ParticleSystem onHitVFX;

        private void OnEnable()
        {
            damageable.onTakeDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            damageable.onTakeDamage -= OnTakeDamage;
        }
        
        private void OnTakeDamage(int damage)
        {
            PlayOnHitVFX();
            Punch();
        }

        private void PlayOnHitVFX()
        {
            var instance = Instantiate(onHitVFX, transform.position, Quaternion.identity);
            instance.transform.localScale *= scaleReference.localScale.x;
            instance.gameObject.SetActive(true);
            instance.Play();
        }

        private void Punch()
        {
            transform.DOComplete();
            transform.DOPunchScale(Vector3.one * punchStrength, punchDuration, punchVibrato, punchElasticity).SetEase(punchEase);
        }
    }
}
