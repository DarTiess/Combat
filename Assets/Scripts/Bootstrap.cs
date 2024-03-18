using Infrastructure.Input;
using TMPro;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
   
    [SerializeField] private Transform _plane;
    [SerializeField] private PlayerView _playerPrefab;
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerRotation;
    [SerializeField] private EnemyView _enemyPrefab;
    [SerializeField] private int _enemySize;

    private Camera _camera;
    private Vector3 _planeSize;
    private PlayerView _player;
    private EnemyFactory _enemyFactory;
    private IInputService _input;

    private void Awake()
    {
        _input = InputService();
        SetCameraSize();
        CreatePlayer();
        CreateEnemy();
    }

    private IInputService InputService()
    {
        if (Application.isEditor)
        {
            return new StandaloneInputService();
        }
        else
        {
            return new MobileInputService();
        }
    }
    private void CreateEnemy()
    {
        _enemyFactory = new EnemyFactory(_enemyPrefab, _enemySize, transform);
        _enemyFactory.SetToPosition(_planeSize);
    }

    private void CreatePlayer()
    {
        _player = Instantiate(_playerPrefab);
        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -_planeSize.z / 3);
        _player.Init(_input,_playerMoveSpeed, _playerRotation);
    }

    private void SetCameraSize()
    {
        _camera = Camera.main;

        _planeSize = _plane.gameObject.GetComponent<MeshRenderer>().bounds.size;
        _camera.orthographicSize = _planeSize.x;
    }
}