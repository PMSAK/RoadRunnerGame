using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    [SerializeField] float RotAngle;

    void Update()
    {
        transform.Rotate(0f, RotAngle, 0f);
    }
}
