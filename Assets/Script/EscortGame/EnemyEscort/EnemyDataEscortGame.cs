using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/EnemyDataEscortGame")]
public class EnemyDataEscortGame : ScriptableObject
{
    public GameObject prefab;

    public float maxHP;
    public float moveSpeed;
    public float damage;
}
