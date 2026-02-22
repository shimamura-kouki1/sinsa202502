using System;
using UnityEngine;

/// <summary>
/// 操作する砲台を管理
/// </summary>
public class TurretSelector : MonoBehaviour
{
    [SerializeField] private TurretController[] _turrets;
    [SerializeField] private Transform _playerTransform;
    public event Action<Transform> OnTurretChanged;

    private int _currentIndex;

    public TurretController CurrentTurret => _turrets[_currentIndex];//Index内の砲台の情報取得
    public void Select(int index)
    {
        if (_turrets == null || _turrets.Length == 0) return;
        if (index < 0 || index >= _turrets.Length) return;

        _currentIndex = index;

        Transform pivot = _turrets[index].transform;
        _playerTransform.position = pivot.position;
        _playerTransform.rotation = pivot.rotation;

        OnTurretChanged?.Invoke(pivot);
    }
}
