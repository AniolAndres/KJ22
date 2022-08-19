using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPolarCurve
{
    Vector3 CalculatePosition(float interpolator);
}
