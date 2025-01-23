using DG.Tweening;
using UnityEngine;

public class MeatballEyeballOnDeathVFX : MonoBehaviour
{
    public void PlayOnDeathVFX()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one * 2.0f, 0.5f).SetEase(Ease.InCubic);
    }
}
