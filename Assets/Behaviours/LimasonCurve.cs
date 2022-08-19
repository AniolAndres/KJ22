using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimasonCurve : IPolarCurve {

    private readonly float constantA;

    private readonly float constantB;

    public LimasonCurve(float cosntantA, float constantB) {
        this.constantA = cosntantA;
        this.constantB = constantB;
    }

    public Vector3 CalculatePosition(float angle) {
        var radius = constantA + constantB * Mathf.Cos(angle);
        return new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
    }
}
