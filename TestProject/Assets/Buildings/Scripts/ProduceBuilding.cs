public class ProduceBuilding : Building // Building that produces resources.
{
    public override void Initialize()
    {
        // Start the process of produce.
    }

    public override void Destroy()
    {
        // Stop processes.
        base.Destroy();
    }
}
