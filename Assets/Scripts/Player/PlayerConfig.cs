using System;
using UnityEngine;

namespace GamePlayObjects.Cat.StateMachine
{
    [CreateAssetMenu(menuName = "Configs/Player", fileName = "PlayerConfig", order = 52)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _health;
        [SerializeField] private float _attackSpeed;
        [SerializeField]
        private BulletConfig _bulletConfig;

        public float MoveSpeed=>_moveSpeed;
        public float RotateSpeed=>_rotateSpeed;
        public float Health=>_health;
        public float AttackSpeed=>_attackSpeed;
        public BulletConfig BulletConfig => _bulletConfig;

    }
}