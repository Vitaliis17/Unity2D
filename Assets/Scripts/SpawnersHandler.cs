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

        Coin[] coins = new[]
        {
        _coinSpawner.GetElement(new(8, -6.6f)),
        _coinSpawner.GetElement(new(6, -3)),
        _coinSpawner.GetElement(new(-14.6f, -2f)),
        _coinSpawner.GetElement(new(13, -5.7f))
        };

        foreach (Coin coin in coins)
            coin.Releasing += Release;

        _medkitSpawner = new Spawner<Medkit>(_medkitPrefab, _medkitContainer);

        Medkit[] medkits = new[]
        {
            _medkitSpawner.GetElement(new(10, -2.7f)),
            _medkitSpawner.GetElement(new(29, 5.3f))
        };

        foreach (Medkit medkit in medkits)
            medkit.Releasing += Release;

        _enemySpawner = new Spawner<Enemy>(_enemyPrefab, _enemyContainer);

        Enemy[] patrolmen = new[]
        {
        _enemySpawner.GetElement(new(13, 6)),
        _enemySpawner.GetElement(new(6.5f, -8))
        };

        foreach (Enemy patrolman in patrolmen)
            patrolman.Releasing += Release;

        _targetPointSpawner = new Spawner<TargetPoint>(_targetPointPrefab, _targetPointContainer);

        TargetPoint[] targetPoints = new[]
        {
        _targetPointSpawner.GetElement(new(13, 5.6f)),
        _targetPointSpawner.GetElement(new(16, 5.6f)),
        _targetPointSpawner.GetElement(new(6, -8)),
        _targetPointSpawner.GetElement(new(12, -8))
        };

        foreach (TargetPoint points in targetPoints)
            points.Releasing += Release;

        patrolmen[0].AddTargetPoints(targetPoints[0], targetPoints[1]);
        patrolmen[1].AddTargetPoints(targetPoints[2], targetPoints[3]);
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
}
