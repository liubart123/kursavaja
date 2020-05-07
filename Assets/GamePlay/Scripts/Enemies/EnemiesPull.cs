using Assets.GamePlay.Scripts.Waves;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Enemies
{
    public class EnemiesPull
    {
        public static ObservableCollection<Enemy> AllEnemies;
        public static void Initialize()
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            AllEnemies = new ObservableCollection<Enemy>();
            AllEnemies.CollectionChanged += CollectionChanged;
        }
        private static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: 
                    break;
                case NotifyCollectionChangedAction.Remove: 
                    if (AllEnemies.Count == 0)
                    {
                        WaveManager.EndWavePossible();
                    }
                    break;
                case NotifyCollectionChangedAction.Replace: 
                    break;
            }
        }
    }
}
