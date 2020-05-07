using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class OnlineManager : MonoBehaviour
    {
        public static bool DoNotOwnCalculations = false;
        public static bool CreateNetworkObjects = false;

        private void Update()
        {
            //DoNotOwnCalculations = !PhotonNetwork.IsMasterClient && PhotonNetwork.IsConnected;
            //CreateNetworkObjects = PhotonNetwork.IsMasterClient && PhotonNetwork.IsConnected;
        }
    }
}
