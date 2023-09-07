using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftHand;

    [SerializeField]
    private GameObject _rightHand;

    private float pressThreshold = 0.8f;
    private bool _leftHandOccupied;
    private bool _rightHandOccupied;

    public GameObject LeftHand
    {
        get { return _leftHand; }
    }

    public GameObject RightHand
    {
        get { return _rightHand; }
    }

    public bool LeftHandOccupied
    {
        get { return _leftHandOccupied; }
        set { _leftHandOccupied = value; }
    }

    public bool RightHandOccupied
    {
        get { return _rightHandOccupied; }
        set { _rightHandOccupied = value; }
    }

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
