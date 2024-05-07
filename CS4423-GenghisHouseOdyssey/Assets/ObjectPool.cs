using UnityEngine; // Required namespace for Unity methods like Instantiate
using System.Collections.Generic; // For generic collections like List and Queue

public class ObjectPool<T> where T : UnityEngine.Object
{
    private Queue<T> pool;
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<T>();

        for (int i = 0; i < initialSize; i++)
        {
            T obj = UnityEngine.Object.Instantiate(prefab, parent); // Correctly using Instantiate
            if (obj is GameObject go)
            {
                go.SetActive(false); // Deactivate the object in the pool
            }
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            if (obj is GameObject go)
            {
                go.SetActive(true); // Activate the object
            }
            return obj;
        }
        T newObject = UnityEngine.Object.Instantiate(prefab, parent);
        return newObject;
    }

    public void Return(T obj)
    {
        if (obj is GameObject go)
        {
            go.SetActive(false); // Deactivate before returning to the pool
        }
        pool.Enqueue(obj);
    }
}
