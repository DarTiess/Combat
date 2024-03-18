using DefaultNamespace;
using UnityEngine;

public class EnemyFactory
{
    private ObjectPoole<EnemyView> _poole;
    private int _size;

    public EnemyFactory(EnemyView prefab,int size, Transform parent)
    {
        _poole = new ObjectPoole<EnemyView>();
        _size = size;
        _poole.CreatePool(prefab,size,parent);
    }

    public void SetToPosition(Vector3 planeSize)
    {
        for (int i = 0; i < _size; i++)
        {
            var rndX = Random.Range(-planeSize.x/2, planeSize.x/2);
            var rndZ = Random.Range(-(planeSize.z / 3), (planeSize.z /2));
            var enemy= _poole.GetObject();
            enemy.transform.position= new Vector3(rndX, enemy.transform.position.y, rndZ);
        }
       
    }
}