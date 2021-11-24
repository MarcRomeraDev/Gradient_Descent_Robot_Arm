using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorMovement : MonoBehaviour
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




    void Update ()
    {
        transform.localRotation = original.transform.localRotation;
    }
}
