using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public abstract void Initialize();
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }    
}
