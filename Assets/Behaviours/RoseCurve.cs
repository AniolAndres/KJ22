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
        var firstPos = new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));

        var secondAngle = angle * 1.5f;
        var secondRadius = constantA * Mathf.Cos((constantB + 1) * secondAngle);
        var secondPos = new Vector2(secondRadius * Mathf.Cos(secondAngle), secondRadius * Mathf.Sin(secondAngle));

        return Vector3.Lerp(firstPos, secondPos, 0.5f);
    }
}
