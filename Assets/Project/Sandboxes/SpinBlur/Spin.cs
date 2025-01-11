using UnityEngine;

namespace Project.z_Delete
{
    public class Spin : MonoBehaviour
    {
        public float speed = 10f;

        private void Update()
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
    }
}
