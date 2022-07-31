using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildPoint 
{
    bool IsFree { get; }

    void Build();
}
