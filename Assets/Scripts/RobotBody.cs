using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class RobotBody : Singleton<RobotBody>
{
    public float MaxWheelTorque = 20;
    public WheelCollider LeftWheel, RightWheel;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }

    // percent must be in range [-1, 1], or an exception will be thrown
    public void TorqueWheel (WheelIndicator wheel, float percent)
    {
        if (Mathf.Abs(percent) > 1)
        {
            throw new System.Exception("Torque percent value " + percent + " is out of range [-1, 1].");
        }

        var torque = MaxWheelTorque * percent;

        switch (wheel)
        {
            case WheelIndicator.Left:
                LeftWheel.motorTorque = torque;
                break;
            case WheelIndicator.Right:
                RightWheel.motorTorque = torque;
                break;
        }
    }
}

public enum WheelIndicator
{
    Left, Right
}
