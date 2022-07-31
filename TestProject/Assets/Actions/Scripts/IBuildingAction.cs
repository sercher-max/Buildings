public interface IBuildingAction : IActionIcon
{       
    public bool IsDone { get; }
    public void MakeAction(IBuildPoint buildPoint);
    public void ForceInterrupt();
}
