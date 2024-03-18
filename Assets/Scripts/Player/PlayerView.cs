using Infrastructure.Input;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerView : MonoBehaviour
{
    private IInputService _input;
    private NavMeshAgent _navMesh;
    private Vector3 temp;
    private float _playerSpeed;
    private float _rotationSpeed;
    // private IMoveAnimation _moveAnimation;
   
    private bool _canMove;
    

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    public void Init(IInputService input,float playerSpeed, float rotationSpeed)
    {
        _input = input;
        _playerSpeed = playerSpeed;
        _rotationSpeed = rotationSpeed;
       // _moveAnimation = moveAnimation;
        _navMesh = GetComponent<NavMeshAgent>();
        StartMoving();
    }

    public void MakeRotation(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        if (lookDirection != Vector3.zero)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                                       Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void PlayerDead()
    {
        _canMove = false;
    }

    private void StartMoving()
    {
        _canMove = true;
    }

    private void Move()
    {
        temp.x = _input.GetHorizontal;
        temp.z = _input.GetVertical;

      //  _moveAnimation.MoveAnimation(temp.magnitude);
        _navMesh.Move(temp * _playerSpeed * Time.fixedDeltaTime);

        //  Vector3 tempDirect = transform.position + Vector3.Normalize(temp);
         MakeRotation(temp);
    }
}