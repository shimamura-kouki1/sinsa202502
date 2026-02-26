using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用型オブジェクトプール
/// </summary>
public class ObjectPool : MonoBehaviour
{
    // キー：Prefab
    // 値：そのPrefabの待機オブジェクトのキュー
    private Dictionary<GameObject, Queue<GameObject>> _pool
        = new Dictionary<GameObject, Queue<GameObject>>();

    /// <summary>
    /// オブジェクトを取得する（再利用 or 新規生成）
    /// </summary>
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // そのPrefab用のキューが存在するかチェック
        if (_pool.TryGetValue(prefab, out var queue) && queue.Count > 0)
        {
            // キューから取り出す
            GameObject obj = queue.Dequeue();

            // 位置と回転を更新
            obj.transform.SetPositionAndRotation(position, rotation);

            // 再表示
            obj.SetActive(true);

            return obj;
        }
        // 無ければ新しく生成
        return Instantiate(prefab, position, rotation);
    }

    /// <summary>
    /// objectとプールに戻す
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="prefab"></param>
    public void Despawn(GameObject obj,GameObject prefab)
    {
        obj.SetActive(false);

        //そのプレハブ用のきゅーがなければ作る
        if(!_pool.ContainsKey(prefab))
        {
            _pool[prefab] = new Queue<GameObject>(); 
        }
        //キューに戻す
        _pool[prefab].Enqueue(obj);
    }

}
