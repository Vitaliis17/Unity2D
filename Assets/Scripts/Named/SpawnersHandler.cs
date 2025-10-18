using UnityEngine;

public class SpawnersHandler : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private TargetPoint _targetPointPrefab;

    [SerializeField] private Transform _coinContainer;
    [SerializeField] private Transform _medkitContainer;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Transform _targetPointContainer;

    private Spawner<Coin> _coinSpawner;
    private Spawner<Medkit> _medkitSpawner;
    private Spawner<Enemy> _enemySpawner;
    private Spawner<TargetPoint> _targetPointSpawner;

    private void Awake()
    {
        _coinSpawner = new Spawner<Coin>(_coinPrefab, _coinContainer);
        Coin[] coins = GetElements(_coinSpawner, new(8, -6.6f), new(6, -3), new(-14.6f, -2f), new(13, -5.7f));

        foreach (Coin coin in coins)
            coin.Releasing += Release;

        _medkitSpawner = new Spawner<Medkit>(_medkitPrefab, _medkitContainer);
        Medkit[] medkits = GetElements(_medkitSpawner, new(10, -2.7f), new(29, 5.3f));

        foreach (Medkit medkit in medkits)
            medkit.Releasing += Release;

        _enemySpawner = new Spawner<Enemy>(_enemyPrefab, _enemyContainer);
        Enemy[] enemies = GetElements(_enemySpawner, new(13, 6), new(6.5f, -8));

        foreach (Enemy patrolman in enemies)
            patrolman.Releasing += Release;

        _targetPointSpawner = new Spawner<TargetPoint>(_targetPointPrefab, _targetPointContainer);
        TargetPoint[] targetPoints = GetElements(_targetPointSpawner, new(13, 5.6f), new(16, 5.6f), new(6, -8), new(12, -8));

        foreach (TargetPoint points in targetPoints)
            points.Releasing += Release;

        int enemyTargetPointAmount = 2;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].TryGetComponent(out Patrolman patrolman))
            {
                patrolman.AddTargetPoints(targetPoints[i * enemyTargetPointAmount], targetPoints[i * enemyTargetPointAmount + 1]);
            }
        }
    }

    public void Release(Component component)
    {
        switch (component)
        {
            case Coin coin:
                coin.Releasing -= Release;
                _coinSpawner.ReleaseElement(coin);
                break;

            case Medkit medkit:
                medkit.Releasing -= Release;
                _medkitSpawner.ReleaseElement(medkit);
                break;

            case Enemy patrolman:
                patrolman.Releasing -= Release;
                _enemySpawner.ReleaseElement(patrolman);
                break;

            case TargetPoint targetPoint:
                targetPoint.Releasing -= Release;
                _targetPointSpawner.ReleaseElement(targetPoint);
                break;
        }
    }

    private T[] GetElements<T>(Spawner<T> spawner, params Vector2[] positions) where T : Component
    {
        T[] components = new T[positions.Length];

        for(int i = 0; i < components.Length; i++)
            components[i] = spawner.GetElement(positions[i]);

        return components;
    }
}
