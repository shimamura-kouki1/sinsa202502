using UnityEngine;

/// <summary>
///敵や護衛対象の移動経路のノード
/// </summary>
public class WaypointNode : MonoBehaviour
{
    [Header("次のノードを入れる")]
    [SerializeField] private WaypointNode[] _nextNodes;

    /// <summary>
    /// 次のノードを取得
    /// </summary>
    /// <returns></returns>
    public WaypointNode GetNextNode()
    {
        if (_nextNodes == null || _nextNodes.Length == 0) return null;

        return _nextNodes[0];
    }

    /// <summary>
    /// 次のノードが存在するか判定
    /// なければfalse
    /// </summary>
    /// <returns></returns>
    public bool NextNodes()
    {
        return _nextNodes != null && _nextNodes.Length > 0;
    }
}
