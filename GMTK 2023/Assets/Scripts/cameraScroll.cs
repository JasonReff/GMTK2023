using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScroll : MonoBehaviour
{
    public float speed = 2;
void Update()
{
    // Moves the object forward at two units per second.
    transform.Translate(Vector3.right * speed * Time.deltaTime);
}
}
