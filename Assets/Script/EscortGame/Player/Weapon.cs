using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _range = 100f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private LayerMask _hitLayer;

    [SerializeField] private GameObject _hitEffect;

    [Header("Tracer設定")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _tracerDuration = 0.05f; // 表示時間
    [SerializeField] private Transform _tracerStratPos;

    [Header("SE")]
    [SerializeField] private AudioClip _fireSE;

    /// <summary>
    /// 発射時に呼ばれる
    /// WweaponSEで購読
    /// </summary>
    public event Action OnFire;

    /// <summary>
    /// ヒット時に呼ばれる
    /// 現在使用してない
    /// </summary>
    public event Action<RaycastHit> OnHit;

    /// <summary>
    /// 射撃処理
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    public void Fire(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);

        Vector3 endPoint = origin + direction * _range; // デフォルト終点

        if (Physics.Raycast(ray, out RaycastHit hit ,_range,_hitLayer))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                //ダメージ
                damageable.TakeDamage(_damage);
            }
            //ヒットエフェクト生成
            if (_hitEffect != null)
            {
                Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

        }
        // 銃口から当たり位置への方向を計算
        Vector3 tracerDir = (endPoint - _tracerStratPos.position).normalized;

        // 銃口から当たり位置までの距離を計算
        float tracerDistance = Vector3.Distance(_tracerStratPos.position, endPoint);

        // 銃口から正しい方向・距離で終点を再計算
        Vector3 tracerEnd = _tracerStratPos.position + tracerDir * tracerDistance;

        // トレーサー表示
        StartCoroutine(ShowTracer(_tracerStratPos.position,tracerEnd));

        FireSE();
    }

    /// <summary>
    /// 弾道トレーサーを一瞬表示する
    /// </summary>
    private IEnumerator ShowTracer(Vector3 start, Vector3 end)
    {
        _lineRenderer.enabled = true;

        _lineRenderer.SetPosition(0, start); // 始点
        _lineRenderer.SetPosition(1, end);   // 終点

        yield return new WaitForSeconds(_tracerDuration);

        _lineRenderer.enabled = false;
    }

    /// <summary>
    /// 発射SEの生成
    /// </summary>
    private void FireSE()
    {
        //今後WeaponSEに責任を移行予定
        AudioManager.Instance.PlaySE(_fireSE);
    }
}
