using Assets.scripts.serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Photon.MyScripts
{
    //адказвае за загрузку мапы ў анлайн гульні
    public class MapForOnline : MonoBehaviour
    {
        public static string jsonOfMapToLoad;
        public static void LoadSavedMap()
        {
            FindObjectOfType<MapSerDeser>().DeserializeMapFromJson(jsonOfMapToLoad);
            OnlineManager.BuildForAllPlayers = true;
        }
    }
}
