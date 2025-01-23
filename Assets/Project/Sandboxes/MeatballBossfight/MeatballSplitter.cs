using Animancer;
using DG.Tweening;
using Project.Sandboxes.ScriptableValues;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Sandboxes.MeatballBossfight
{
    [RequireComponent(typeof(Damageable))]
    public class MeatballSplitter : MonoBehaviour
    {
        [SerializeField] private ClipTransition deathAnimation;
        [SerializeField] private int splitCount = 2;
        [SerializeField] private IntValue initialHealthValue;
        [SerializeField] private bool _isFirstMeatball;

        private Damageable _damageable;
        
        private bool _hasBeenSplit;

        private void Awake()
        {
            _damageable = GetComponent<Damageable>();

            if (_isFirstMeatball)
            {
                _damageable.InstantiateHealth(initialHealthValue);
                
                ScaleMeatball(gameObject, initialHealthValue);
                _damageable.Health.OnValueChanged += operationResult =>
                {
                    if (operationResult.newValue <= initialHealthValue.Value * 0.5f)
                    {
                        _damageable.Health.OnValueChanged = null;
                        Split(gameObject);
                    }
                };
            }
        }

        private void Split(GameObject oldMeatball)
        {
            for (var i = 0; i < splitCount; i++)
            {
                // Create a new meatball
                var randomPosAroundOrigin = GetRandomPosAroundOrigin(oldMeatball.transform.position);
                var prefab = (GameObject)Resources.Load("Meatball");
                var newMeatball = Instantiate(prefab, randomPosAroundOrigin, Quaternion.identity);
                var newMeatballSplitter = newMeatball.GetComponent<MeatballSplitter>();
                newMeatballSplitter._isFirstMeatball = false;

                // Set the new meatball's health to the old meatball's health
                var oldHealthValue = oldMeatball.GetComponent<Damageable>().Health;
                var newMeatballDamageable = newMeatball.GetComponent<Damageable>();
                newMeatballDamageable.InstantiateHealth(oldHealthValue);
                
                var newHealthValue = newMeatballDamageable.Health;
                
                // Scale the new meatball based on its health value
                ScaleMeatball(newMeatball, newHealthValue);
                
                // Set the new meatball's movement speed based on its health value (lower health = faster)
                var meatballMovement = newMeatball.GetComponent<MeatballMovement>();
                meatballMovement.SetMovementSpeed(newHealthValue);
                
                var splitHealthThreshold = oldHealthValue.Value * 0.5f;
                newMeatballDamageable.Health.OnValueChanged += operationResult =>
                {
                    if (operationResult.newValue <= 0)
                    {
                        DestroyMeatball(newMeatball);
                    }
                    else if (operationResult.newValue <= splitHealthThreshold)
                    {
                        Split(newMeatball.gameObject);    
                    }
                };
            }

            DestroyMeatball(oldMeatball);
        }
        
        private void DestroyMeatball( GameObject meatball )
        {
            // meatball.GetComponentsInChildren<MeatballEyeballOnDeathVFX>().ForEach(eyeball => eyeball?.PlayOnDeathVFX());
            // meatball.GetComponent<MeatballAnimationVFX>().PlayOnDeathVFX();
            meatball.GetComponent<MeatballExplodeOnDeath>().Explode();
            meatball.transform.DOComplete();
            Destroy(meatball.gameObject, 0.05f);
            // var animancer = meatball.GetComponent<AnimancerComponent>();
            // animancer.Stop();
            // animancer.Play(deathAnimation);
        }

        private static Vector3 GetRandomPosAroundOrigin(Vector3 origin)
        {
            var randomPosA = Random.insideUnitCircle * Random.Range(1, 2);
            var randomPosB = Random.insideUnitCircle * Random.Range(1, 2);
            return origin + new Vector3(randomPosA.x, randomPosB.x, randomPosA.y);
        }
        
        private void ScaleMeatball(GameObject meatball, IntValue healthValue)
        {
            var scaleT = Mathf.InverseLerp(healthValue.GetMinValue(), healthValue.GetMaxValue(), healthValue.Value);
            var scaleModifier = Mathf.Lerp(0.1f, 1.0f, scaleT);
            meatball.transform.localScale *= scaleModifier;
        }
    }
}
