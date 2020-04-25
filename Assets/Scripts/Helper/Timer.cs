using UnityEngine;

/*
 * A Simple Timer to count Time easier
 */

public class Timer
{
    private float timeAtStart = 0.0f;
    private float timeAtStop = 0.0f;
    private bool running = false;
    private bool paused = false;
    
    public Timer()
    {
        Reset();
    }

    public void Start()
    {
        if (running)
        {
            if (paused)
            {
                paused = false;

                float runnedTime = timeAtStop - timeAtStart;

                timeAtStart = Time.time - runnedTime;
            }
        } else
        {
            running = true;
            timeAtStart = Time.time;
        }
    }

    public void Pause()
    {
        if (running && !paused)
        {
            timeAtStop = Time.time;
            paused = true;
        }
    }

    public void Stop()
    {
        if (running)
        {
            if (paused)
            {
                paused = false;
            } else
            {
                timeAtStop = Time.time;
            }

            running = false;
        }
    }

    public void Reset()
    {
        timeAtStart = 0;
        timeAtStop = 0;
        running = false;
        paused = false;
    }

    public void Restart()
    {
        Reset();
        Start();
    }

    public void Subtract(float subtractionAmount)
    {
        timeAtStart += subtractionAmount;
    }

    public float Get()
    {
        if (paused || !running)
        {
            return timeAtStop - timeAtStart;
        } else
        {
            return Time.time - timeAtStart;
        }
    }

    public bool GetStarted()
    {
        return running;
    }

    public bool GetPaused()
    {
        return paused;
    }
}
