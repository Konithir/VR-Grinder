using UnityEngine;

public class GrinderBlade : MonoBehaviour
{
    [SerializeField]
    private GrinderController _grinderController;

    public GrinderController GrinderController
    {
        get { return _grinderController; }
    }
}
