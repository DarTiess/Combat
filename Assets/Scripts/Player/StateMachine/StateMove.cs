using DefaultNamespace.Player;
using Infrastructure.Input;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlayObjects.Cat.StateMachine
{
    public class StateMove: State
    {
        private IInputService _inputService;
        private IMoveAnimation _moveAnimator;
        private Vector3 _temp;
        private NavMeshAgent _nav;
        private Transform _transform;
        private float _playerSpeed;
        private float _rotationSpeed;

        public StateMove(StateMachine stateMachine, IInputService inputService, NavMeshAgent navMesh, 
                         Transform transformParent,IMoveAnimation moveAnimator,
                              PlayerConfig config): base(stateMachine)
        {
            _inputService=inputService; 
            _moveAnimator = moveAnimator;
            _nav = navMesh;
            _transform = transformParent;
            _playerSpeed = config.MoveSpeed;
            _rotationSpeed = config.RotateSpeed;
        }

        public override void Enter()
        {
           Debug.Log("StateMove");
        }

        public override void Update()
        {
           OnMove();
        }

        private void OnMove()
        {
            float inputHorizontal = _inputService.GetHorizontal;
            float inputVertical = _inputService.GetVertical;
       
            _temp.x = inputHorizontal;
            _temp.z = inputVertical;
            
            _moveAnimator.MoveAnimation(_temp.magnitude);
            _nav.Move(_temp * _playerSpeed * Time.deltaTime);
            Rotation();
            if (inputHorizontal == 0 && inputVertical == 0)
            {
                _stateMachine.SetState<StateAttack>();
            }
           
          
        }

        private void Rotation()
        {
            Vector3 tempDirect = _transform.position + Vector3.Normalize(_temp);
            Vector3 lookDirection = tempDirect - _transform.position;
            if (lookDirection != Vector3.zero)
            {
                _transform.localRotation = Quaternion.Slerp(_transform.localRotation,
                                                            Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.deltaTime);
            }
        }
    }
}