using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class WheelDriver : Singleton<WheelDriver>
{
    public float MaxWheelTorque = 20;

    public AnimationCurve AccelCurve, DecelCurve;
    public float AccelTime => AccelCurve.keys[AccelCurve.keys.Length - 1].time;
    public float DecelTime => DecelCurve.keys[DecelCurve.keys.Length - 1].time;

    [SerializeField]
    WheelCollider leftWheel = null, rightWheel = null; // initialize in inspector

    private class individualWheelDriver
    {
        public float Power;
        public bool Throttled = true;
        public bool ThrottledChanged;

        public float Torque => wheel.motorTorque;

        WheelCollider wheel;
        float accelTimeMax, accelTimer;
        bool celerating; // accelerating or decelerating

        public individualWheelDriver (WheelCollider wheel)
        {
            this.wheel = wheel;
        }

        public void ApplyTorque (float dt)
        {
            var wd = WheelDriver.Instance;

            if (ThrottledChanged)
            {
                accelTimeMax = Throttled ? wd.DecelTime : wd.AccelTime;
                ThrottledChanged = false;
                celerating = true;
            }

            if (celerating)
            {
                if (accelTimer < accelTimeMax)
                {
                    var curve = Throttled ? wd.DecelCurve : wd.AccelCurve;
                    wheel.motorTorque = Power * wd.MaxWheelTorque * curve.Evaluate(accelTimer);
                    accelTimer += dt;
                }
                else
                {
                    Power = Throttled ? 0 : 1;
                    celerating = false;
                }
            }
            else
            {
                wheel.motorTorque = Power * wd.MaxWheelTorque;
            }
        }
    }
    individualWheelDriver leftDriver, rightDriver;

    void Awake ()
    {
        leftDriver = new individualWheelDriver(leftWheel);
        rightDriver = new individualWheelDriver(rightWheel);
        SingletonSetInstance(this, true);
    }

    void Update ()
    {
        var dt = Time.deltaTime;
        leftDriver.ApplyTorque(dt);
        rightDriver.ApplyTorque(dt);
    }

    public void SetPower (WheelIndicator wheel, float power)
    {
        assertPercent(power);
        switch (wheel)
        {
            case WheelIndicator.Left:
                leftDriver.Power = power;
                break;
            case WheelIndicator.Right:
                rightDriver.Power = power;
                break;
            default:
                throw new System.Exception("Unexpected default case while switching on " + wheel);
        }
    }

    public void SetThrottled (WheelIndicator wheel, bool throttled)
    {
        switch (wheel)
        {
            case WheelIndicator.Left:
                if (throttled != leftDriver.Throttled)
                {
                    leftDriver.Throttled = throttled;
                    leftDriver.ThrottledChanged = true;
                }
                break;
            case WheelIndicator.Right:
                if (throttled != rightDriver.Throttled)
                {
                    rightDriver.Throttled = throttled;
                    rightDriver.ThrottledChanged = true;
                }
                break;
            default:
                throw new System.Exception("Unexpected default case while switching on " + wheel);
        }
    }

    void assertPercent (float percent)
    {
        if (Mathf.Abs(percent) > 1)
        {
            throw new System.Exception("Torque percent value " + percent + " is out of range [-1, 1].");
        }
    }
}

public enum WheelIndicator
{
    Left, Right
}

public static class WheelIndicatorExtensions
{
    public static string ToShortString (this WheelIndicator self)
    {
        switch (self)
        {
            case WheelIndicator.Left:
                return "L";
            case WheelIndicator.Right:
                return "R";
            default:
                throw new System.Exception("Unexpected default case while switching on " + self);
        }
    }
}
