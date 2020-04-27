using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [Tooltip("After wich time should the first object be spawned")]
    public float firstSpawnAfter = 0.0f;
    [Tooltip("Spawn only once or with an set Interval? (Only change BEFORE gamestart)")]
    public bool endlessSpawner = false;

    [MyBox.ConditionalField("endlessSpawner")]
    public float spawnInterval = 1.0f;

    public GameObject spawnPrefab;
    public bool addRigidbodyAtSpawn = false;

    public bool useCustomSpawnLocation = false;
    [MyBox.ConditionalField("useCustomSpawnLocation")]
    public Vector3 customSpawnLocation;

    public bool enableOnSpawn = false;

    

    private Timer startTimer;
    private bool startTimerOver = false;

    private Timer spawnIntervalTimer;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = new Timer();
        startTimer.Start();

        spawnIntervalTimer = new Timer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startTimerOver)
        {
            if (startTimer.Get() >= firstSpawnAfter)
            {
                startTimerOver = true;
                startTimer = null;
                spawnIntervalTimer.Start();
                Spawn();
            }

            return;

        }

        if (endlessSpawner)
        {
            while (spawnIntervalTimer.Get() >= spawnInterval)
            {
                spawnIntervalTimer.Subtract(spawnInterval);
                Spawn();
            }
        }

    }

    void Spawn()
    {
        GameObject spawnedObject;

        if (useCustomSpawnLocation && customSpawnLocation != null)
        {
            spawnedObject = Instantiate(spawnPrefab, customSpawnLocation, Quaternion.identity);
        } else
        {
            spawnedObject = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        }

        if (addRigidbodyAtSpawn)
        {
            spawnedObject.AddComponent<Rigidbody>();
        }

        if (enableOnSpawn)
        {
            spawnedObject.SetActive(true);
        }
    }

    void Pause()
    {
        if (startTimerOver)
        {
            startTimer.Pause();
        } else
        {
            spawnIntervalTimer.Pause();
        }
    }

    void Continue()
    {
        if (startTimerOver)
        {
            startTimer.Start();
        } else
        {
            spawnIntervalTimer.Start();
        }
    }
}
