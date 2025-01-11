using UnityEngine;

[RequireComponent(typeof(Health))]
public class SpawnPrefabOnDeathExample : MonoBehaviour
{
    [SerializeField] private GameObject mSpawnPrefab;

    private void OnEnable()
    {
        GetComponent<Health>().onKill += OnKill;
    }

    private void OnDisable()
    {
        GetComponent<Health>().onKill -= OnKill;
    }

    private void OnKill()
    {
        Instantiate(mSpawnPrefab, transform.position, mSpawnPrefab.transform.rotation);
        Destroy(gameObject);
    }

}
