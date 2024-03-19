using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFactory : IGetNearestEnemy
{
    private ObjectPoole<EnemyView> _poole;
    private int _size;
    private readonly EnemyConfig _config;
    private List<EnemyView> _enemyList;

    public EnemyFactory(EnemyConfig config)
    {
        _config = config;
        _enemyList = new List<EnemyView>(_config.Count);
        _poole = new ObjectPoole<EnemyView>();
        _size = _config.Count;
        _poole.CreatePool(_config.Prefab,_size,null);
    }

    public void SetToPosition(Vector3 planeSize)
    {
        for (int i = 0; i < _size; i++)
        {
            var rndX = Random.Range(-planeSize.x/2, planeSize.x/2);
            var rndZ = Random.Range(-(planeSize.z / 3), (planeSize.z /2));
            var enemy= _poole.GetObject();
            enemy.transform.position= new Vector3(rndX, enemy.transform.position.y, rndZ);
            _enemyList.Add(enemy);
        }
       
    }
    public Transform GetNearestEnemy(Transform target)
    {
        Transform enemyPosition = null;

        float distanceMin = 100;
        foreach (EnemyView enemy in _enemyList)
        {
            float distance = Vector3.Distance(target.position, enemy.gameObject.transform.position);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                enemyPosition = enemy.gameObject.transform;
            }
        }
        return enemyPosition;
    }
}