using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderInteractionPoint : MonoBehaviour
{
    [SerializeField]
    private float _cuttingProgressNeeded = 10;

    [SerializeField]
    private List<Rigidbody> _rigidbodyList;

    [SerializeField]
    private GameObject _cuttingIndicator;

    [SerializeField]
    private GameObject _sparks;

    private float _cuttingProgress;
    private GrinderBlade _grinderBlade;
    private bool _isCut = false;

    private void Start()
    {
        _sparks.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isCut)
        {
            return;
        }

        if(_grinderBlade == null && other.CompareTag("Grinder"))
        {
            _grinderBlade = other.GetComponent<GrinderBlade>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Grinder") && other.GetComponent<GrinderBlade>() == _grinderBlade)
        {
            _grinderBlade = null;
            TurnSparks(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isCut)
        {
            return;
        }

        if (other.CompareTag("Grinder") && _grinderBlade != null && _grinderBlade.GrinderController.IsWorking && _grinderBlade.GrinderController.MainGrabbingPoint.IsGrabbed && _grinderBlade.GrinderController.SecondaryGrabbingPoint.IsGrabbed)
        {
            _cuttingProgress += Time.deltaTime;
            TurnSparks(true);

            if (_cuttingProgress >= _cuttingProgressNeeded)
            {
                FinalizeCut();
            }
        }
        else if(other.CompareTag("Grinder") && _grinderBlade != null)
        {
            TurnSparks(false);
        }
    }

    private void FinalizeCut()
    {
        _isCut = true;
        TurnSparks(false);

        for (int i = 0; i < _rigidbodyList.Count; i++)
        {
            _rigidbodyList[i].isKinematic = false;
            _rigidbodyList[i].useGravity = true;
            _cuttingIndicator.SetActive(false);
        }
    }

    private void TurnSparks(bool value)
    {
        _sparks.gameObject.SetActive(value);
    }
}
