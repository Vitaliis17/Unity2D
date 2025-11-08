public class HealthPresenter : Presenter
{
    private readonly Health _health;
    private readonly SmoothBar _bar;

    public HealthPresenter(Health heatlh, SmoothBar bar)
    {
        _health = heatlh;
        _bar = bar;

        _health.MaxValueChanged += _bar.SetMax;
        _health.CurrentValueChanged += _bar.SetValueSmoothly;
    }

    public override void RemoveListeners()
    {
        _health.MaxValueChanged -= _bar.SetMax;
        _health.CurrentValueChanged -= _bar.SetValueSmoothly;
    }
}