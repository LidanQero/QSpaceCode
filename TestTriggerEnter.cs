using UnityEngine;

namespace Master.QSpaceCode
{
    public class TestTriggerEnter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterController>()) return;
            Debug.Log(other.name);
        }
    }
}