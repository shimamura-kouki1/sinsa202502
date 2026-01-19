using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDate")]
public class EnemyDate : ScriptableObject
{
    public Transform _despoilAnchor;
    public float _maxHp = 100;
    public float _attackValue;
}
