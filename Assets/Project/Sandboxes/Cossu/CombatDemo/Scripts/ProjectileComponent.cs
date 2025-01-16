using System;
using UnityEngine;

public abstract class ProjectileComponent : MonoBehaviour
{
    protected TarotDataObject tarotDataObject;
    protected ProjectileEvents projectileEvents;
    bool SetupDone = false;

    public void Setup(TarotDataObject dataObject, ProjectileEvents projectileEventsComponent)
    {
        tarotDataObject = dataObject;
        projectileEvents = projectileEventsComponent;
        SetupDone = true;
        OnSetup();
    }

    private void Update()
    {
        if(SetupDone) ProjectileUpdate();
    }

    private void FixedUpdate()
    {
        if(SetupDone) ProjectileFixedUpdate();
    }

    public virtual void ProjectileUpdate() //Projectile update behaviour here
    {

    }

    public virtual void ProjectileFixedUpdate()
    {

    }

    public virtual void OnSetup() //On Setup behaviour. Subscribe to events etc if you want to.
    {

    } 
}
