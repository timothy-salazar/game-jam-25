using UnityEngine;

public class RemoveRigidBody : MonoBehaviour
{
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (body.linearVelocity.magnitude <= 1)
        {
            Destroy(body);
            Destroy(this);
        }
    }
}
