
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlliedShipList", menuName = "ScritpableObject/AlliedShipList", order = 100)]
public class AlliedShipProvider : ScriptableObject
{
    [SerializeField]
    private List<ShipData> shipData; 
    
    public ShipData GetFirstShip() {
        return shipData[0];
    }

    public ShipData GetSecondShip() {
        return shipData[1];
    }

    public ShipData GetThirdShip() {
        return shipData[2];
    }
}
