using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public GameObject original;

    [Header("Angle Constraint")]
    public bool AngleConstraintActive;
    public bool CancelTwist;

    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    public Transform parent;
    public Transform child;

    [Header("Plane Constraint")]
    public bool PlaneConstraintActive;
    public bool debugLines;

    public Transform plane;

    // To define how "strict" we want to be
    private float threshold = 0.00001f;

    void Update()
    {
        GetSwing(original.transform.localRotation).ToAngleAxis(out float angle, out Vector3 axis);

        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        if (CancelTwist)
            transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(angle, minAngle, maxAngle), axis);
        else
            transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(angle, minAngle, maxAngle), axis) * GetTwist(original.transform.localRotation);
    }

    public Quaternion GetTwist(Quaternion q)
    {
        return Quaternion.Normalize(new Quaternion(0, q.y, 0, q.w));
    }

    public Quaternion GetSwing(Quaternion q)
    {
        return q * Quaternion.Inverse(GetTwist(q));
    }
}