using DefaultNamespace.Player;
using GamePlayObjects.Cat.StateMachine;
using Infrastructure.Input;
using Infrastructure.Level.EventsBus;
using Infrastructure.Level.EventsBus.Signals;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private PlayerConfig _playerConfig;
    [SerializeField] private Transform _bulletPlace;
    
    private NavMeshAgent _navMesh;
    private Animator _animator;
    private bool _canMove;
    private PlayerAnimation _playerAnimation;
    private IEventBus _eventBus;
    private IInputService _input;
    private StateMachine _stateMachine;
    private IGetNearestEnemy _nearestEnemy;

    [Inject] public void Construct(IInputService input, IEventBus eventBus, PlayerConfig config, IGetNearestEnemy nearestEnemy)
    {
        _nearestEnemy = nearestEnemy;
        _eventBus = eventBus;
        _input = input;
        _playerConfig = config;
    }
    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(_animator);
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new StateAttack(_stateMachine, _input, _playerConfig, _bulletPlace, _nearestEnemy));
        _stateMachine.AddState(new StateMove(_stateMachine,_input, _navMesh, transform, _playerAnimation, _playerConfig));
        _stateMachine.SetState<StateMove>();
       _eventBus.Subscribe<PlayGame>(StartGame);
    }

    private void StartGame(PlayGame obj)
    {
        StartMoving();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            _stateMachine.Update();
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

    public void TakeDamage(float attackPower)
    {
        Debug.Log("Player Take Damage");
    }
}