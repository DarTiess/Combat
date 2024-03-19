using DefaultNamespace;
using Infrastructure.Input;
using UnityEngine;

namespace GamePlayObjects.Cat.StateMachine
{
    public class StateAttack: State
    {
        private readonly IInputService _input;
        private ObjectPoole<Bullet> _poole;
        private readonly PlayerConfig _config;
        private float _attackTimer;
        private readonly Transform _originPosition;
        private readonly Transform _parent;
        private readonly IGetNearestEnemy _nearestEnemy;
        private Transform _target;

        public StateAttack(StateMachine stateMachine, IInputService input, PlayerConfig config,Transform parent , IGetNearestEnemy nearestEnemy) 
            : base(stateMachine)
        {
            _input = input;
            _config = config;
            _originPosition = parent;
            _parent = _originPosition.GetComponentInParent<PlayerView>().transform;
            _nearestEnemy = nearestEnemy;
            _poole = new ObjectPoole<Bullet>();
            _poole.CreatePool(_config.BulletConfig.BulletPrefab, _config.BulletConfig.BulletCount, parent);

        }
        public override void Enter()
        {
            Debug.Log("StateAttack");
            _attackTimer = 0;
            _target = _nearestEnemy.GetNearestEnemy(_originPosition);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            if (_input.GetHorizontal != 0 || _input.GetVertical != 0)
            {
                _stateMachine.SetState<StateMove>();
            }

            if (_target == null)
            {
                _target= _nearestEnemy.GetNearestEnemy(_parent);
                Debug.Log(_target);
            }
            else
            {
                MakeRotation(_target.position);
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= _config.AttackSpeed)
                {
                    var bullet=_poole.GetObject();
                    bullet.transform.SetPositionAndRotation(_originPosition.position, _originPosition.rotation);
                    bullet.transform.parent = null;
                    bullet.Initialize(_config.BulletConfig);
                    _attackTimer = 0;
                } 
            }
         
          

        }

        private void MakeRotation(Vector3 target)
        {
            Vector3 lookDirection = target - _parent.position;
            if (lookDirection != Vector3.zero)
            {
                _parent.localRotation = Quaternion.Slerp(_parent.localRotation,
                                                           Quaternion.LookRotation(lookDirection),
                                                           _config.RotateSpeed * Time.fixedDeltaTime);
            }
        }


      
    }
}