using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> where T : Component
{
    private readonly ObjectPool<T> _pool;

    private readonly Transform _container;
    private readonly T _prefab;

    public Spawner(T prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;

        _pool = new(Create, Get, Release, Destroy);
    }

    public void ReleaseElement(T element)
        => _pool.Release(element);

    public T GetElement(Vector2 position)
    {
        T element = _pool.Get();
        element.transform.position = position;

        return element;
    }

    private T Create()
    {
        T element = Object.Instantiate(_prefab, _container);
        element.name = _prefab.name;

        return element;
    }

    private void Get(T element)
        => element.gameObject.SetActive(true);

    private void Release(T element)
        => element.gameObject.SetActive(false);

    private void Destroy(T element)
        => Object.Destroy(element.gameObject);
}
