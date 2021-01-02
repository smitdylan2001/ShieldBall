using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hittable
{
    bool IsHit { get; }
    void GetsHit();
}
