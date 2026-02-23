using System.Collections;
using System.Net;
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
        // トレーサー表示
        StartCoroutine(ShowTracer(origin, endPoint));
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
}
