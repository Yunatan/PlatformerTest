using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    private List<Transform> onPlatform = new List<Transform>();

    private Vector3 lastPos;

    void OnCollisionEnter2D(Collision2D other)
    {
        var body = other.rigidbody;
        if (body == null) return;
        onPlatform.Add(other.transform);
        other.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        onPlatform.Remove(other.transform);
        other.transform.parent = null;
    }

    void Start()
    {
        lastPos = transform.position;
    }
}
