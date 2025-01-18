using System;
using UnityEngine;

[Serializable]
public abstract class SerializableMonobehaviour
{
    protected SerializableMonoBehaviourContainer container;
    protected GameObject gameObject;
    protected Transform transform => gameObject.transform;

    public void Initialize(SerializableMonoBehaviourContainer initializingContainer)
    {
        container = initializingContainer;
        gameObject = initializingContainer.gameObject;
        Start();
    }

    virtual public void Start() { }

    virtual public void Update() { }

    virtual public void FixedUpdate() { }
}
