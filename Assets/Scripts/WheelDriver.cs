using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class WheelDriver : Singleton<WheelDriver>
{
    public float MaxWheelTorque = 20;

    [SerializeField]
    WheelCollider leftWheel = null, rightWheel = null; // initialize in inspector

    private class WheelData
    {
        public float? PercentRegister;
        public float TorqueMemory;
        public bool Idle;
    }
    WheelData leftData, rightData;

    void Awake ()
    {
        leftData = new WheelData();
        rightData = new WheelData();
        SingletonSetInstance(this, true);
    }

    void Update ()
    {
        if (leftData.Idle)
        {
            leftWheel.motorTorque = leftData.TorqueMemory;
        }
        if (rightData.Idle)
        {
            rightWheel.motorTorque = rightData.TorqueMemory;
        }
    }

    // percent must be in range [-1, 1], or an exception will be thrown
    public void TorqueWheel (WheelIndicator wheel, float percent)
    {
        assertPercent(percent);

        var torque = MaxWheelTorque * percent;

        switch (wheel)
        {
            case WheelIndicator.Left:
                leftWheel.motorTorque = torque;
                leftData.TorqueMemory = torque;
                break;
            case WheelIndicator.Right:
                rightWheel.motorTorque = torque;
                rightData.TorqueMemory = torque;
                break;
        }
    }

    public float GetWheelTorquePercent (WheelIndicator wheel)
    {
        switch (wheel)
        {
            case WheelIndicator.Left:
                return leftWheel.motorTorque / MaxWheelTorque;
            case WheelIndicator.Right:
                return rightWheel.motorTorque / MaxWheelTorque;
            default: // should not happen
                throw new System.Exception("Reached unexpected default in switch statement while switching on " + wheel);
        }
    }

    public void TryTorqueBoth (float? left, float? right)
    {
        if (left != null)
        {
            WheelDriver.Instance.TorqueWheel(WheelIndicator.Left, (float) left);
        }

        if (right != null)
        {
            WheelDriver.Instance.TorqueWheel(WheelIndicator.Right, (float) right);
        }
    }

    public void StoreWheelPercent (WheelIndicator wheel, float percent)
    {
        assertPercent(percent);
        switch (wheel)
        {
            case WheelIndicator.Left:
                leftData.PercentRegister = percent;
                break;
            case WheelIndicator.Right:
                rightData.PercentRegister = percent;
                break;
        }
    }

    public float? PopWheelPercents (WheelIndicator wheel)
    {
        float? val;
        switch (wheel)
        {
            case WheelIndicator.Left:
                val = leftData.PercentRegister;
                leftData.PercentRegister = null;
                return val;
            case WheelIndicator.Right:
                val = rightData.PercentRegister;
                rightData.PercentRegister = null;
                return val;
            default: // should not happen
                throw new System.Exception("Reached unexpected default in switch statement while switching on " + wheel);
        }
    }

    public void SetIdleState (bool state, WheelIndicator firstWheel, params WheelIndicator[] otherWheels)
    {
        switch (firstWheel)
        {
            case WheelIndicator.Left:
                leftData.Idle = state;
                break;
            case WheelIndicator.Right:
                rightData.Idle = state;
                break;
        }
        if (otherWheels.Length > 0)
        {
            SetIdleState(state, otherWheels[0], otherWheels.Skip(1).ToArray());
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
