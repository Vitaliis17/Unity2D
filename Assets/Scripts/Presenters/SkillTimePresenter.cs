public class SkillTimePresenter : Presenter
{
    private readonly VampirismSkill _vampirism;
    private readonly SmoothBar _bar;

    public SkillTimePresenter(VampirismSkill vampirism, SmoothBar bar)
    {
        _vampirism = vampirism;
        _bar = bar;

        _vampirism.MaxValueChanged += _bar.SetMax;
        _vampirism.CurrentValueChanged += _bar.SetValueSmoothly;
    }

    public override void RemoveListeners()
    {
        _vampirism.MaxValueChanged -= _bar.SetMax;
        _vampirism.CurrentValueChanged -= _bar.SetValueSmoothly;
    }
}