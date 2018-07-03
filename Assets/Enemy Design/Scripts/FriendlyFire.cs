using UnityEngine;

public class FriendlyFire : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(10, 9);
    }
}
