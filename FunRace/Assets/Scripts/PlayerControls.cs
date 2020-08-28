using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public CatmullRomSpline spline;
    public float speed = 0.5f;
    public float start = 0.0f;
    public float t = 0.0f;
    public float offsetY = 1f;
    public int segment = 0;
    public bool last = false;
    public bool collided = false;

    private Rigidbody rb;

    private bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!spline.isLooping)
        {
            segment = 1;
        }

        rb.MovePosition(spline.GetCurvePos(t, segment) + new Vector3(0, offsetY, 0));
        transform.forward = spline.GetCurveForward(t, segment);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ready)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            t += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            t -= speed * Time.deltaTime;
        }

        if (t >= 1.0f)
        {
            ++segment;

            if (segment == spline.controlPointsList.Length - 2)
            {
                segment = spline.controlPointsList.Length - 3;
                t = 1.0f;
            }
            else
            {
                t = 0.0f;
            }
        }

        if (t < 0.0f)
        {
            --segment;
            if (segment == 0)
            {
                segment = 1;
                t = 0.0f;
            }
            else
            {
                t = 1.0f;
            }
        }

        if (segment == spline.controlPointsList.Length - 3)
        {
            last = true;
        }

        rb.MovePosition(spline.GetCurvePos(t, segment) + new Vector3(0, offsetY, 0));
        transform.forward = spline.GetCurveForward(t, segment);
    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }

    public void SetPlayerEnabled(bool b)
    {
        ready = b;
    }

    public bool GetPlayerEnabled()
    {
        return ready;
    }
}
