using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurve : MonoBehaviour
{
    public CatmullRomSpline spline;
    public float speed = 1.0f;
    public float start = 0.0f;

    private float t = 0.0f;
    private int segment = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!spline.isLooping)
        {
            segment = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = spline.GetCurvePos(t, segment);
        t -= speed * Time.deltaTime;
        if (t >= 1.0f)
        {
            ++segment;
            if (spline.isLooping && segment == spline.controlPointsList.Length)
            {
                segment = 0;
            }
            else if (!spline.isLooping && segment == spline.controlPointsList.Length - 2)
            {
                segment = 1;
            }
            t = 0.0f;
        }

        if (t < 0.0f)
        {
            --segment;
            if (spline.isLooping && segment < 0)
            {
                segment = spline.controlPointsList.Length - 1;
            }
            else if (!spline.isLooping && segment < 1)
            {
                segment = spline.controlPointsList.Length - 3;
            }
            t = 1.0f;
        }
    }
}
