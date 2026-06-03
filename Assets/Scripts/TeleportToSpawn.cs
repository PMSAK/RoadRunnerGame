using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToSpawn : MonoBehaviour
{
    public Transform spawnPoint;

    [SerializeField] private Timer timer;

    void Update()
    {
        Respawn();
    }

    void Respawn()
    {
        if (transform.position.y <= -10)
        {
            transform.position = spawnPoint.position;
            
            timer.ResetTimer();
        }
    }
}
