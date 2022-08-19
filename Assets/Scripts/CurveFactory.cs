using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class CurveFactory {

    public IPolarCurve GetRandomCurve() {
        var random = Random.Range(0, 3);
        switch (random) {
            case 0:
                return new RoseCurve(150,5);
            case 1:
                return new RoseCurve(150,7);
            case 2:
                return new RoseCurve(150,3);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
