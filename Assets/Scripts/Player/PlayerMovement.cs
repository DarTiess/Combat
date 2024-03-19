using DefaultNamespace.Player;
using Infrastructure.Input;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement
{
    private IInputService _inputService;
    private IMoveAnimation _moveAnimator;
    private Vector3 _temp;
    private NavMeshAgent _nav;
    private Transform _transform;
    private float _playerSpeed;
    private float _rotationSpeed;

    public PlayerMovement(IInputService inputService, NavMeshAgent navMesh, 
                          Transform transformParent,IMoveAnimation moveAnimator,
                          float speedMove, float speedRotate)
    {
        _inputService=inputService; 
        _moveAnimator = moveAnimator;
        _nav = navMesh;
        _transform = transformParent;
        _playerSpeed = speedMove;
        _rotationSpeed = speedRotate;
    }
    public void OnMove()
    {
        float inputHorizontal = _inputService.GetHorizontal;
        float inputVertical = _inputService.GetVertical;
       
        _temp.x = inputHorizontal;
        _temp.z = inputVertical;
            
         _moveAnimator.MoveAnimation(_temp.magnitude);
        _nav.Move(_temp * _playerSpeed * Time.deltaTime);
           
        Rotation();
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