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

    private float _cuttingProgress;
    private GrinderBlade _grinderBlade;
    private bool _isCut = false;

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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isCut)
        {
            return;
        }

        if (other.CompareTag("Grinder") && _grinderBlade != null && _grinderBlade.GrinderController.IsWorking)
        {
            _cuttingProgress += Time.deltaTime;
            Debug.LogError(_cuttingProgress);

            if (_cuttingProgress >= _cuttingProgressNeeded)
            {
                Debug.LogError("Cut!");
                FinalizeCut();
            }
        }
    }

    private void FinalizeCut()
    {
        _isCut = true;

        for (int i = 0; i < _rigidbodyList.Count; i++)
        {
            _rigidbodyList[i].isKinematic = false;
            _rigidbodyList[i].useGravity = true;
            _cuttingIndicator.SetActive(false);
        }
    }
}
