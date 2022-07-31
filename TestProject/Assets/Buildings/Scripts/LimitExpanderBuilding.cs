public class LimitExpanderBuilding : Building // Building that expands resource limits.
{
    public override void Initialize()
    {
        // Add bonus to one or several resource limits.
    }

    public override void Destroy()
    {
        // Remove this building's bonus.
        base.Destroy();
    }
}
