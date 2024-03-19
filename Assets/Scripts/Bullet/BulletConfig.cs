using System;
using UnityEngine;

namespace GamePlayObjects.Cat.StateMachine
{
    [CreateAssetMenu(menuName = "Configs/Bullet", fileName = "BulletConfig", order = 52)]   
    public class BulletConfig : ScriptableObject
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletCount;
        [SerializeField] private Material _material;
        [SerializeField] private BulletTypes _type;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _attackPower;
        
        public Bullet BulletPrefab=>_bulletPrefab;
        public int BulletCount=>_bulletCount;
        public Material Material=>_material;
        public BulletTypes Type=>_type;
        public float Speed=>_speed;
        public float LifeTime=>_lifeTime;
        public float AttackPower=>_attackPower;
    }
}