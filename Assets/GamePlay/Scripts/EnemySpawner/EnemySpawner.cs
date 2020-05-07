using Assets;
using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Enemies.Interfaces.MovingTargetChooser;
using Assets.GamePlay.Scripts.Enemies.Interfaces.PathFinder;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Building
{
    public GameObject enemyToSpawn;
    public int cooldown;
    private int cooldownCounter = 0;
    public int delay;
    private int delayCounter;
    public string enemiesName = "enemie";

    //parameters
    public float enemieCountKaef;
    public float enemieCooldownKaef;


    //SPAWNING
    private void FixedUpdate()
    {
        //if (OnlineManager.CreateNetworkObjects)
        //    return;
        if (isSpawning && delayCounter++ < delay)
        {
            return;
        }
        if (isSpawning && cooldownCounter++ > cooldown)
        {
            cooldownCounter = 0;
            SpawnEnemy();
        }
        if (enemieCounter > enemiMaxCount)
        {
            FinishSpawning();
        }
    }
    private int enemieCounter = 0;  //count of enemies, that were spawned this wave
    private static int enemieSpawnerCounter = 0;    //nu,ber of spawner
    public bool isSpawning;  
    public int enemiMaxCount;    //max count of enemie to spawn this wave
    protected int waveNumber;
    public virtual void StartSpawning()
    {
        enemieCounter = 0;
        ChooseTargetForMoving();
        CreatePath();
        isSpawning = true;
    }
    public virtual void FinishSpawning()
    {
        isSpawning = false;
        enemyMovingTargetChooser.Reset();
    }
    public void SpawnEnemy()
    {
        GameObject enemy;
        if (OnlineManager.DoNotOwnCalculations)
        {
            enemy = PhotonNetwork.Instantiate(enemyToSpawn.name, transform.position, transform.rotation);
        }
        else { enemy = Instantiate(enemyToSpawn, transform.position, transform.rotation); }


        enemy.GetComponent<Enemy>().pathFromSpawner = currentPath;
        enemy.GetComponent<Enemy>().Initialize();
        enemy.gameObject.name = enemiesName + enemieCounter++ + "_" + enemieSpawnerCounter;

        EnemiesPull.AllEnemies.Add(enemy.GetComponent<Enemy>());
        enemy.GetComponent<Enemy>().eventsWhenThisDie += RemoveEnemyFromCollectionAfterDeath;
    }
    private static void RemoveEnemyFromCollectionAfterDeath(Enemy enemy)
    {
        EnemiesPull.AllEnemies.Remove(enemy);
    }



    //MOVING
    public EnemyMovingTargetChooser enemyMovingTargetChooser;
    protected ICollection<Block> currentPath;
    protected Building targetToMove;
    public virtual void ChooseTargetForMoving()
    {
        targetToMove = enemyMovingTargetChooser.ChooseTargetForMove(
            new EnemyMovingTargetChooserParameters(transform.position));
        CreatePath();
    }
    protected virtual void CreatePath()
    {
        if (targetToMove != null)
        {
            PathFinder pf = new PathFinder();
            currentPath = pf.GetPath(
                GetBlock(),
                targetToMove.GetBlock());
        }
        else
        {
            currentPath = null;
        }
    }
    public override void Initialize()
    {
        if (OnlineManager.CreateNetworkObjects)
            return;
        enemieSpawnerCounter++;
        enemyMovingTargetChooser = GetComponent<EnemyMovingTargetChooser>();

        base.Initialize();
    }
}
