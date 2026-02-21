using UnityEngine;

/// <summary>
/// 操作する砲台を管理
/// </summary>
public class TurretSelector : MonoBehaviour
{
    [SerializeField] private TurretController[] _turrets;
    [SerializeField] private Transform _cameraTransform;

    private int _currentIndex;

    public TurretController CurrentTurret => _turrets[_currentIndex];//Index内の砲台の情報取得
    public void Select(int index)
    {
        if (_turrets == null || _turrets.Length == 0) return;
        if (index < 0 || index >= _turrets.Length) return;

        _currentIndex = index;
        _cameraTransform.position = _turrets[index].transform.position;
        _cameraTransform.rotation = _turrets[index].transform.rotation;
    }
}
