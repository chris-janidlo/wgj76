using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRigRotator : MonoBehaviour
{
    public Button Left, Right;

    void Start()
    {
        Left.onClick.AddListener(rotateLeft);
        Right.onClick.AddListener(rotateRight);
    }

    void rotate (bool dirFlag)
    {
        var direction = dirFlag ? -1 : 1;
        transform.Rotate(Vector3.up * direction * (45f / 4f));
    }

    void rotateLeft ()
    {
        rotate(true);
    }

    void rotateRight ()
    {
        rotate(false);
    }
}
