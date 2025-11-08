using UnityEngine;

public class BootStraper : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private VampirismSkill _vampirism;

    [SerializeField] private SmoothBar _healthBar;
    [SerializeField] private SmoothBar _vampirismBar;

    private HealthPresenter _healthPresenter;
    private SkillTimePresenter _skillTimePresenter;

    private void Awake()
    {
        _healthPresenter = new(_health, _healthBar);
        _skillTimePresenter = new(_vampirism, _vampirismBar);
    }

}