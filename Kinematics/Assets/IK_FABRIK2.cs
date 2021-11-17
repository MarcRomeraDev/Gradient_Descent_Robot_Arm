using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;
    float threshHold = 0.03f;
    int maxIterations = 15;
    int iterations = 15;
    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];

        for (int i = 0; i < joints.Length - 1; i++)
        {
            distances[i] = Vector3.Distance(joints[i].position, joints[i + 1].position);
        }
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO
        for (int i = 0; i < joints.Length; i++)
        {
            copy[i] = joints[i].position;
        }
        //done = TODO
        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);
            Vector3 targetGoal = target.position;
            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                targetGoal = copy[0]+(target.position - copy[0]).normalized * distances.Sum();
            }
            iterations = maxIterations;
            // The target is reachable
            while (Vector3.Distance(copy[copy.Length - 1], targetGoal) > threshHold && iterations > 0)
            {
                // STAGE 1: FORWARD REACHING
                //TODO
                copy[copy.Length - 1] = targetGoal;
                for (int i = copy.Length - 2; i >= 0; i--)
                    copy[i] = copy[i + 1] + ((copy[i] - copy[i + 1]).normalized * distances[i]);


                // STAGE 2: BACKWARD REACHING
                //TODO
                copy[0] = joints[0].position;
                for (int i = 1; i > copy.Length; i++)
                    copy[i] = copy[i - 1] + ((copy[i] - copy[i-1]).normalized * distances[i]);

                iterations--;
            }
            for (int i = 0; i < copy.Length - 1; i++)
                Debug.DrawLine(copy[i], copy[i+1],Color.green,0,false);
            iterations = maxIterations;
            // Update original joint rotations
          
                for (int i = 0; i < joints.Length - 1; i++)
                {

                    Vector3 currentDirection = joints[i].transform.TransformDirection(Vector3.up).normalized;
                    Vector3 endDirection = (copy[i + 1] - copy[i]).normalized;

              

                   // if (Vector3.Dot(currentDirection, endDirection) > 1 - 0.01) continue; // está alineado

                    Vector3 axis = Vector3.Cross(currentDirection, endDirection).normalized;

                    float angle = Mathf.Acos((Vector3.Dot(currentDirection, endDirection)));
                    angle *= Mathf.Rad2Deg;

                    joints[i].transform.Rotate(axis, angle, Space.World);
                }
               // iterations--;
            
        }
    }

}
