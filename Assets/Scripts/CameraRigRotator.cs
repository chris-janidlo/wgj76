using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRigRotator : MonoBehaviour
{
    public float RotationSpeed;
    public Button Left, Right, Reset;

    Quaternion targetRotation;

    void Start()
    {
        Left.onClick.AddListener(rotateLeft);
        Right.onClick.AddListener(rotateRight);
        Reset.onClick.AddListener(reset);
        targetRotation = transform.rotation;
    }

    void Update ()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    void rotate (bool dirFlag)
    {
        var direction = dirFlag ? -1 : 1;
        targetRotation *= Quaternion.Euler(Vector3.up * direction * (45f / 4f));
    }

    void rotateLeft ()
    {
        rotate(true);
    }

    void rotateRight ()
    {
        rotate(false);
    }

    void reset ()
    {
        targetRotation = Quaternion.identity;
    }
}
