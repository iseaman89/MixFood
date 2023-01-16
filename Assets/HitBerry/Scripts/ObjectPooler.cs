using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public ObjectPooler(Food objectToPool, int amountToPool)
    {
        _objectToPool = objectToPool;
        _amountToPool = amountToPool;
        
        FillPool();
    }
    
    #region Variables

    private static ObjectPooler s_instance;
    private readonly List<Food> _pooledObjects = new List<Food>();
    
    private readonly Food _objectToPool;
    private readonly int _amountToPool;

    #endregion

    #region Functions

    private Food InstantiatePoolObject()
    {
        var newObject = Instantiate(_objectToPool);
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    private void FillPool()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            AddObjectInPool(InstantiatePoolObject());
        }
    }

    private void AddObjectInPool(Food poolObject) => _pooledObjects.Add(poolObject);
    
    public Food GetPooledObject()
    {
        foreach (var food in _pooledObjects)
        {
            if (!food.gameObject.activeInHierarchy) return food;
        }

        var newObject = InstantiatePoolObject();
        AddObjectInPool(newObject);
        return newObject;
    }

    public void DestroyPoolObjects()
    {
        foreach (var food in _pooledObjects)
        {
            Destroy(food.gameObject);
        }
    }

    #endregion
}