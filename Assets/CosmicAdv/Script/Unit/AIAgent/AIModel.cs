using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AIAgent;
using Utility;
using CA_Terrain;

public class AIModel : MonoBehaviour {
    private List<Unit_STP> unitSTPlist = new List<Unit_STP>();
    private List<AIAgent> agentlist = new List<AIAgent>();
    MapGenerator map;

    public void SetUp(MapGenerator p_mapGenerator)
    {
        map = p_mapGenerator;
    }

}
