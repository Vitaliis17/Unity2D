using UnityEngine;
using System.Collections;

public class VampirismSkiller : MonoBehaviour
{
    [SerializeField] private int _healthAmount;

    private Coroutine _coroutine;

    public void TransferHealth(Health taker, Health giver)
    {
        float skillTime = 6f;

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Transfer(taker, giver, skillTime));
    }

    private IEnumerator Transfer(Health taker, Health giver, float skillTime)
    {
        while(skillTime > 0f && giver.IsAlive())
        {
            yield return null;

            giver.TakeDamage(_healthAmount);
            taker.Heal(_healthAmount);

            skillTime -= Time.deltaTime;
        }
    }
}