using GamePlayObjects.Cat.StateMachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy", fileName = "EnemyConfig", order = 52)]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private EnemyView _prefab;
    [SerializeField] private int _count;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _radius;
    [SerializeField] private BulletConfig _bulletConfig;

    public int Count=> _count;
    public float MoveSpeed=>_moveSpeed;
    public float RotateSpeed=>_rotateSpeed;
    public float Health=>_health;
    public float AttackSpeed=>_attackSpeed;
    public float Radius=>_radius;
    public BulletConfig BulletConfig => _bulletConfig;
    public EnemyView Prefab => _prefab;
}