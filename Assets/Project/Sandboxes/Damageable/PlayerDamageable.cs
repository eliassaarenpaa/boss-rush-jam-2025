namespace Project.Sandboxes
{
    public class PlayerDamageable : Damageable
    {
        private void Awake()
        {
            InstantiateHealth(Health);
        }
    }
}
