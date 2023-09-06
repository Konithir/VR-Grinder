using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsManager : MonoBehaviour
{

    private float pressThreshold = 0.8f;


    public bool GetLeftHandGrabbingPressed()
    {
        if (Input.GetAxis("XRI_Left_Grip") > pressThreshold)
        {
            return true;
        }
        return false;
    }

    public bool GetRightHandGrabbingPressed()
    {
        if (Input.GetAxis("XRI_Right_Grip") > pressThreshold)
        {
            return true;
        }
        return false;
    }

    public bool GetLeftHandPrimaryPressed()
    {
        if (Input.GetAxis("XRI_Left_Trigger") > pressThreshold)
        {
            return true;
        }

        return false;
    }

    public bool GetRightHandPrimaryPressed()
    {
        if (Input.GetAxis("XRI_Right_Trigger") > pressThreshold)
        {
            return true;
        }

        return false;
    }
}
