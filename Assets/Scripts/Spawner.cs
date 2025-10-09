using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> where T : Component
{
    private ObjectPool<T> _pool;

    private Transform _container;
    private T _prefab;

    public Spawner(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;

        _pool = new(Create, Get, Release, Destroy);
    }

    public void ReleaseElement(T element)
        => _pool.Release(element);

    private T Create()
        => Object.Instantiate(_prefab, _container);

    private void Get(T element)
        => element.gameObject.SetActive(true);

    private void Release(T element)
        => element.gameObject.SetActive(false);

    private void Destroy(T element)
        => Object.Destroy(element.gameObject);
}
