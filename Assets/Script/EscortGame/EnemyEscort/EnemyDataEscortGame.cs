using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/EnemyDataEscortGame")]
public class EnemyDataEscortGame : ScriptableObject
{
    public GameObject prefab;
    public GameObject deathEffectPrefab;
    public AudioClip deathSE;

    [Header("Status")]
    public float maxHP;
    public float moveSpeed;
    public float damage;

    [Header("Score")]
    public int score;
}
