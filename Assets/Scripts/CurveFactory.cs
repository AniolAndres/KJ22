using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class CurveFactory {

    public IPolarCurve GetRandomCurve() {
        var random = Random.Range(0, 3);
        switch (random) {
            case 0:
                return new RoseCurve(250,5);
            case 1:
                return new RoseCurve(250,7);
            case 2:
                return new RoseCurve(250,3);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
