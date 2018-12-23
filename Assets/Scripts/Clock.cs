using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Clock : Singleton<Clock>
{
    [Tooltip("How many seconds happen between each tick.")]
    public float TickTime;
    public float CountdownTimer { get; private set; }
    public float PercentLeftUntilTick { get { return CountdownTimer / TickTime; } }

    public event System.Action OnTick;

    void Awake()
    {
        SingletonSetInstance(this, true);
        CountdownTimer = TickTime;
    }

    void Update()
    {
        CountdownTimer -= Time.deltaTime;

        if (CountdownTimer <= 0)
        {
            CountdownTimer = TickTime;
            if (OnTick != null) OnTick();
        }
    }
}
