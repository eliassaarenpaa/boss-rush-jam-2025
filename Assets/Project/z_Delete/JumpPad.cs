using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float force;

    private readonly Dictionary<Collider, float> _bufferedColliders = new Dictionary<Collider, float>();

    private void Update()
    {
        // Remove from buffer if it's the key has been in there for more than 0.1 seconds
        var keys = new List<Collider>(_bufferedColliders.Keys);
        const float colliderBufferTime = 0.05f;
        foreach (var key in keys.Where(key => Time.time - _bufferedColliders[key] > colliderBufferTime))
        {
            _bufferedColliders.Remove(key);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_bufferedColliders.ContainsKey(other))
        {
            return;
        }

        var rb = other.GetComponent<Rigidbody>();
        if (rb)
        {
            _bufferedColliders.Add(other, Time.time); // Add to buffer, so we don't apply force multiple times

            rb.AddForce(transform.up * (Mathf.Abs(rb.velocity.y) + force), ForceMode.Impulse);
        }
    }
}
