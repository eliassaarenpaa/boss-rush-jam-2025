using UnityEngine;

public class ProjectileEvents : SerializableMonobehaviour
{
    public delegate void OnCollisionEnter(Collision collision);
    public delegate void OnCollisionStay(Collision collision);
    public delegate void OnCollisionExit(Collision collision);
}

public class TestClass : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        
    }
}