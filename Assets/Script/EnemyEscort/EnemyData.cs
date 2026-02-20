using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;

    public float maxHP;
    public float moveSpeed;
    public float damage;
}
