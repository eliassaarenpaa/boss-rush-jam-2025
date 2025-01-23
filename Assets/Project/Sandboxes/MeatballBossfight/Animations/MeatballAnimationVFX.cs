using UnityEngine;

public class MeatballAnimationVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem onDeathVFX;
    
    /// <summary>
    ///     Called by the animation event in the MeatballDeath animation.
    /// </summary>
    public void PlayOnDeathVFX()
    {
        var instance = Instantiate(onDeathVFX, transform.position, Quaternion.identity);
        instance.gameObject.SetActive(true);
        instance.Play();
    }
}
