using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class RobotBody : Singleton<RobotBody>
{
    void Awake ()
    {
        SingletonSetInstance(this, true);
    }
}
