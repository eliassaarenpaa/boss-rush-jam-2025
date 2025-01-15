using UnityEngine;
using Project.Runtime.Core.Input;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] TarotDataObject currentTarot;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] GameObject projectileBasePrefab;

    private void Update()
    {
        //Check input
        if (PlayerInput.Attack)
        {
            //Attack code
        }
    }
}