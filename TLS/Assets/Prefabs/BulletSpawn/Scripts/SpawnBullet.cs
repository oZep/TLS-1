using UnityEngine;
using System.Collections.Generic;
using System;

public class TimedBulletSpawner : MonoBehaviour
{
    [Serializable]
    public class BulletSpawnEvent
    {
        public float spawnDelay;        // Time after previous bullet
        public float angleDegrees;      // Angle around Y axis
        public GameObject bulletType;   // Bullet prefab
    }

    public List<BulletSpawnEvent> spawnSchedule; // change spawn list with the inspec
    public float spawnRadius = 5f;

    private float timeSinceLastSpawn = 0f;
    private int currentIndex = 0;

    void Update()
    {
        if (currentIndex >= spawnSchedule.Count)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        BulletSpawnEvent currentEvent = spawnSchedule[currentIndex];

        if (timeSinceLastSpawn >= currentEvent.spawnDelay)
        {
            SpawnBullet(currentEvent);
            currentIndex++;
            timeSinceLastSpawn = 0f;
        }
    }

public Transform pivotCenter; // Assign in inspector (e.g., the center of the cylinder)

void SpawnBullet(BulletSpawnEvent evt)
{
    float rad = evt.angleDegrees * Mathf.Deg2Rad;

    // Spawn relative to the pivotCenter
    Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * spawnRadius;
    Vector3 spawnPosition = pivotCenter.position + offset;

    spawnPosition.y = transform.position.y; // Keep height from spawner

    Instantiate(evt.bulletType, spawnPosition, Quaternion.identity);
    Debug.Log($"Spawned bullet at {spawnPosition} (angle {evt.angleDegrees})");
}

}
