using System;
using UnityEngine;

namespace GamePlayObjects.Cat.StateMachine
{
    public class Bullet : MonoBehaviour
    {
        private MeshRenderer _renderer;
        private float _startTime;
        private BulletTypes _type;
        private float _speed;
        private float _lifeTime;
        private float _attackPower;

        public void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                gameObject.SetActive(false);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            switch (_type)
            {
                case BulletTypes.FromEnemy:
                    if (other.TryGetComponent<PlayerView>(out PlayerView player))
                    {
                        player.TakeDamage(_attackPower);
                    }
                    break;
                case BulletTypes.FromPlayer:
                    if (other.TryGetComponent<EnemyView>(out EnemyView enemy))
                    {
                        enemy.TakeDamage(_attackPower);
                    }
                    break;
            }
            gameObject.SetActive(false);
        }

        public void Initialize(BulletConfig config)
        {
            _attackPower = config.AttackPower;
            _type = config.Type;
            _speed = config.Speed;
            _lifeTime = config.LifeTime;
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material= config.Material;

            _startTime = Time.realtimeSinceStartup;
        }

    }
}