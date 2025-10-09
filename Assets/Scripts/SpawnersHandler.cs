using UnityEngine;

public class SpawnersHandler : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private Patrolman _patrolmanPrefab;

    [SerializeField] private Transform _coinContainer;
    [SerializeField] private Transform _medkitContainer;
    [SerializeField] private Transform _patrolmanContainer;

    private Spawner<Coin> _coinSpawner;
    private Spawner<Medkit> _medkitSpawner;
    private Spawner<Patrolman> _patrolmanSpawner;

    private void Awake()
    {
        _coinSpawner = new Spawner<Coin>(_coinPrefab, _coinContainer);
        _medkitSpawner = new Spawner<Medkit>(_medkitPrefab, _medkitContainer);
        _patrolmanSpawner = new Spawner<Patrolman>(_patrolmanPrefab, _patrolmanContainer);
    }

    public void Release(Component component)
    {
        switch (component)
        {
            case Coin coin:
                _coinSpawner.ReleaseElement(coin);
                break;

            case Medkit medkit:
                _medkitSpawner.ReleaseElement(medkit);
                break;

            case Patrolman patrolman:
                _patrolmanSpawner.ReleaseElement(patrolman);
                break;
        }
    }
}
