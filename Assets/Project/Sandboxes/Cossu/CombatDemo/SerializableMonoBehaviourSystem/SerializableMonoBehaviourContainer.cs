using UnityEngine;
using System.Collections.Generic;
public class SerializableMonoBehaviourContainer : MonoBehaviour
{
    [SerializeReference] List<SerializableMonobehaviour> serializableMonoBehaviours = new List<SerializableMonobehaviour>();

    public void AddSerializableMonoBehaviour<T>(T mono) where T : SerializableMonobehaviour
    {
        serializableMonoBehaviours.Add(mono);
        mono.Initialize(this);
    }

    public void AddSerializableMonoBehaviour<T>(List<T> monos) where T : SerializableMonobehaviour
    {
        foreach(SerializableMonobehaviour mono in monos)
        {
            serializableMonoBehaviours.Add(mono);
            mono.Initialize(this);
        }
    }

    public T GetSerializableComponent<T>() where T : SerializableMonobehaviour
    {
        foreach(SerializableMonobehaviour mono in serializableMonoBehaviours)
        {
            if(mono is T component)
            {
                return component;
            }
        }
        return null;
    }

    public List<T> GetSerializableComponents<T>() where T : SerializableMonobehaviour
    {
        List<T> returnComponents = new List<T>();
        foreach (SerializableMonobehaviour mono in serializableMonoBehaviours)
        {
            if (mono is T component)
            {
                returnComponents.Add(component);
            }
        }
        return returnComponents;
    }

    private void Update()
    {
        foreach(SerializableMonobehaviour mono in serializableMonoBehaviours)
        {
            mono.Update();
        }
    }

    private void FixedUpdate()
    {
        foreach (SerializableMonobehaviour mono in serializableMonoBehaviours)
        {
            mono.FixedUpdate();
        }
    }
}
