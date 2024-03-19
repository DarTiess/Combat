using System;
using GamePlayObjects.Cat.StateMachine;
using UI;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Transform _plane;
    [SerializeField] private PlayerView _playerPrefab;
    [SerializeField] private PlayerConfig _playerConfig;
   
    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private UIControl _uiPrefab;

    private Camera _camera;
    private Vector3 _planeSize;
    private PlayerView _player;
    private EnemyFactory _enemyFactory;
    private UIControl _ui;


    public override void InstallBindings()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig);
        Container.Bind<EnemyConfig>().FromInstance(_enemyConfig);
       
        SetCameraSize();
        CreateEnemy();
        CreatePlayer();
        CreateUIWindow();
    }

    private void CreateUIWindow()
    {
        _ui = Container.InstantiatePrefabForComponent<UIControl>(_uiPrefab);
        Container.BindInterfacesAndSelfTo<UIControl>().FromInstance(_ui).AsSingle();
    }

    private void CreatePlayer()
    {
        var spawnPosition = new Vector3(0, 0, -_planeSize.z / 3);
        _player = Container.InstantiatePrefabForComponent<PlayerView>(_playerPrefab, spawnPosition, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<PlayerView>().FromInstance(_player).AsSingle();
    }

    private void CreateEnemy()
    {
       // _enemyFactory = new EnemyFactory(_enemyPrefab, _enemySize, transform);
      _enemyFactory= Container.Instantiate<EnemyFactory>();
        Container.BindInterfacesAndSelfTo<EnemyFactory>().FromInstance(_enemyFactory).AsSingle();
        _enemyFactory.SetToPosition(_planeSize);
    }

    private void SetCameraSize()
    {
        _camera = Camera.main;
        _planeSize = _plane.gameObject.GetComponent<MeshRenderer>().bounds.size;
        _camera.orthographicSize = _planeSize.x;
    }
}