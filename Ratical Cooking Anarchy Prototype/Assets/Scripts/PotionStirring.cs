using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PotionStirring : MonoBehaviour
{
    Quaternion originalRotation;
    Quaternion previousRotation;

    float startAngle;
    float lastAngle;
    float newAngle;
    int framecount = 0;
    float[] rotations;

    public float avgspeed = 0;
    public Text speedtxt;

    public float targetspeed = 7;
    public float leeway = 5;
    public Text statustxt;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        startAngle = 0f;
        lastAngle = transform.rotation.eulerAngles.z;
        rotations = new float[10];
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion itemRotation = transform.localRotation;

        Quaternion deltaRotation = itemRotation * Quaternion.Inverse(previousRotation);

        previousRotation = itemRotation;

        deltaRotation.ToAngleAxis(out var angle, out var axis);

        angle *= Mathf.Deg2Rad;

        Vector3 angularVelocity = (1.0f / Time.deltaTime) * angle * axis;

        rotations[framecount] = angularVelocity.z < 0 ? -angularVelocity.z : 0;
        framecount++;
        if (framecount >= rotations.Length)
        {
            float sum = 0;
            for (int i = 0; i < rotations.Length; i++)
            {
                sum += rotations[i];
            }
            avgspeed = sum / rotations.Length;
            speedtxt.text = "" + avgspeed;
            if(Mathf.Abs(avgspeed - targetspeed) > leeway)
            {
                if (avgspeed > targetspeed) statustxt.text = "slow down!";
                else statustxt.text = "speed up!";
            }
            else
            {
                statustxt.text = "great job!";
            }
            framecount = 0;
        }
    }

    private void OnMouseDown()
    {
        originalRotation = transform.rotation;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 vector = Input.mousePosition - screenPos;
        startAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    private void OnMouseDrag()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 vector = Input.mousePosition - screenPos;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle - startAngle, transform.forward);
        newRotation.y = 0;
        newRotation.eulerAngles = new Vector3(0, 0, newRotation.eulerAngles.z);
        transform.rotation = originalRotation * newRotation;
    }
}
