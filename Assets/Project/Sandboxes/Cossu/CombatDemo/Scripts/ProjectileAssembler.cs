using UnityEngine;

public class ProjectileAssembler : MonoBehaviour
{
    private ProjectileDataHolder projectileDataHolder;

    private void Start()
    {
        projectileDataHolder = GetComponent<ProjectileDataHolder>();
        
        
    }
}
