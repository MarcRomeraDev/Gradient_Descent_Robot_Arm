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
    

    void Start()
    {
        distances = new float[joints.Length - 1];
        copy = new Vector3[joints.Length];

        for (int i = 0; i < joints.Length-1; i++)
        {
            distances[i] = Vector3.Distance(joints[i].position,joints[i+1].position);
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

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable

            }
            else
            {
                float threshHold = 0.33f;
                int maxIterations = 15;
                // The target is reachable
                while (Vector3.Distance( joints[joints.Length - 1].position,target.position) > threshHold && maxIterations > 0)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO
                    joints[joints.Length -1].position = target.position;
                    for (int i = joints.Length-2; i >= 0; i--)
                        joints[i].position = joints[i+1].position + ((joints[i].position - joints[i+1].position).normalized * distances[i]);


                    // STAGE 2: BACKWARD REACHING
                    //TODO
                    joints[0].position = copy[0];
                    for (int i = 1; i > joints.Length; i++)
                        joints[i].position = joints[i - 1].position + ((joints[i].position - joints[i - 1].position).normalized * distances[i]);

                    maxIterations--;
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                //TODO 
              

            }          
        }
    }

}
