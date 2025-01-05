using Project.Runtime.Gameplay.Player.Controller;
using Project.Runtime.Gameplay.Shared.Physics;
using UnityEngine;

namespace Project.Runtime.Gameplay.Player
{
    [RequireComponent(typeof(PlayerGravity))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterGroundCheck))]
    public class PlayerComponent : MonoBehaviour
    {
        protected Rigidbody Rigidbody;
        protected CharacterGroundCheck GroundCheck;
        protected PlayerGravity Gravity;
        
        protected void Awake()
        {
            GroundCheck = GetComponent<CharacterGroundCheck>();
            Rigidbody = GetComponent<Rigidbody>();
            Gravity = GetComponent<PlayerGravity>();
        }
    }
}
