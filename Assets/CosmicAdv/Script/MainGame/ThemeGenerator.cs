using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CA_Terrain;

public class ThemeGenerator : MonoBehaviour {
    public Theme_STP themeSTP;

    public void ResetTheme(Theme_STP p_theme) {
        themeSTP = p_theme;
        GeneratePoolingObject();
    }

    public void GeneratePoolingObject() {
        if (themeSTP == null)
        {
            Debug.LogError("ThemeSTP not assign");
            return;
        }

        //All type of terrain;
        int poolNum = 18;
        foreach (GameObject t_prefab in themeSTP.terrainPrefab)
        {
            TerrainBuilder tBuilder = t_prefab.GetComponent<TerrainBuilder>();
            PoolManager.instance.CreatePool(t_prefab, tBuilder.terrain_stp._id, poolNum);
        }

        //Other object (Vehicle, Obstacle, Units)
        foreach (STPObject t_object in themeSTP.stpObjectHolder)
        {
            PoolManager.instance.CreatePool(t_object.prefab, t_object._id, t_object.poolingNum);
        }
    }

    public List<T> GetObjectByType<T>() where T : STPObject {
        List<T> objectList = new List<T>();
        for(int i = 0; i < themeSTP.stpObjectHolder.Count; i++) {
            if (themeSTP.stpObjectHolder[i].GetType() == typeof(T)) {
                objectList.Add((T) themeSTP.stpObjectHolder[i]);
            }
        }
        return objectList;
    }
    

}
