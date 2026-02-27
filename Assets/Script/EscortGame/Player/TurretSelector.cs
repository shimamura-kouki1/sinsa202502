using System;
using UnityEngine;

/// <summary>
/// 操作する砲台を管理
/// </summary>
public class TurretSelector : MonoBehaviour
{
    [SerializeField] private TurretController[] _turrets;//管理する砲台
    [SerializeField] private Transform _playerTransform;//プレイヤー本体

    /// <summary>
    /// 砲台切り替え時に呼ばれる（引数：選択された砲台のtransform）
    /// PlayerControllerで購読
    /// </summary>
    public event Action<Transform> OnTurretChanged;

    private int _currentIndex;//現在の砲台

    /// <summary>
    /// 指定されたインデックスの砲台の情報取得
    /// </summary>
    public TurretController CurrentTurret => _turrets[_currentIndex];//Index内の砲台の情報取得

    /// <summary>
    /// 指定したインデックスの砲台を選択する
    /// </summary>
    /// <param name="index"></param>
    public void Select(int index)
    {
        //配列が空かnull
        if (_turrets == null || _turrets.Length == 0) return;
        //インデックスの範囲外虫
        if (index < 0 || index >= _turrets.Length) return;

        //現在のインデックス更新
        _currentIndex = index;

        //プレイヤーのTransformを砲台の位置と回転に合わせる
        Transform pivot = _turrets[index].transform;
        _playerTransform.position = pivot.position;
        _playerTransform.rotation = pivot.rotation;

        //砲台切り替えの通知
        OnTurretChanged?.Invoke(pivot);
    }
}
