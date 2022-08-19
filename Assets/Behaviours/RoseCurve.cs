using UnityEngine;

public class RoseCurve : IPolarCurve
{
    private readonly float constantA;

    private readonly float constantB;

    public RoseCurve(float constantA, float constantB) {
        this.constantA = constantA;
        this.constantB = constantB;
    }

    public Vector3 CalculatePosition(float angle) {
        var radius = constantA * Mathf.Cos(constantB * angle);
        return new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
    }
}
