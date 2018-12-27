using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class RobotBody : Singleton<RobotBody>
{
    public const float MAX_HEALTH = 100;

    public float Health { get; private set; } = MAX_HEALTH;
    
    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    public Tuple<string, string> HealthStatusString ()
    {
        string text, color;
        if (Health >= 80)
        {
            text = "Good";
            color = "green";
        }
        else if (Health >= 60)
        {
            text = "Fine";
            color = "green";
        }
        else if (Health >= 40)
        {
            text = "Dmgd";
            color = "orange";
        }
        else
        {
            text = "ALERT";
            color = "red";
        }

        return new Tuple<string, string>(text, color);
    }
}
