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

    public event Action OnFire;
    public event Action<RaycastHit> OnHit;

    /// <summary>
    /// レイを飛ばす
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
                Debug.Log("hit");
                damageable.TakeDamage(_damage);
            }
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

    private void FireSE()
    {
        AudioManager.Instance.PlaySE(_fireSE);
    }
}
