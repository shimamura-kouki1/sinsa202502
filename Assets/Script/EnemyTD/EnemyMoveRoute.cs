using UnityEngine;

public class WaypointNode : MonoBehaviour
{
    [Header("ŽŸ‚Ìƒm[ƒh‚ð“ü‚ê‚é")]
    [SerializeField] private WaypointNode[] _nextNodes;

    public WaypointNode GetNextNode()
    {
        if (_nextNodes == null || _nextNodes.Length == 0) return null;

        return _nextNodes[0];
    }

    public bool NextNodes()
    {
        return _nextNodes != null && _nextNodes.Length > 0;
    }
}
