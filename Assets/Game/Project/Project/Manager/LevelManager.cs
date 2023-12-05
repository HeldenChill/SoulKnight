using Game;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Utilities.AI;
using Utilitys.Timer;

namespace Project
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager inst;
        public static LevelManager Inst => inst;
        [SerializeField]
        private Map map;
        [SerializeField]
        Transform[] spawnPositions;
        [SerializeField]
        GameObject enemy;
        [SerializeField]
        float spawnTime = 0.25f;
        [SerializeField]
        float enemyNum = 10;

        List<ChasingEnemy> enemyPools = new List<ChasingEnemy>();
        STimer spawnTimer;
        public Map Map => map;
        void Awake()
        {
            if (inst == null)
            {
                inst = this;
                spawnTimer = TimerManager.Inst.PopSTimer();
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            InitLevel();
        }
        private void InitLevel()
        {
            spawnTimer.Start(spawnTime, SpawnEnemy, true);
            void SpawnEnemy()
            {
                ChasingEnemy enemyObj = PopEnemy() as ChasingEnemy;
                int index = Random.Range(0, spawnPositions.Length);
                enemyObj.transform.position = spawnPositions[index].position;
                enemyObj.OnInit();

                if(ChasingEnemy.Count >= enemyNum)
                {
                    spawnTimer.Stop();
                }
            }
        }

        protected IPoolUnit PopEnemy()
        {
            ChasingEnemy enemyScripts = enemyPools.Find(x => x.gameObject.activeInHierarchy == false);
            if(enemyScripts == null)
            {
                for(int i = 0; i < 10; i++)
                {
                    enemyScripts = Instantiate(enemy, transform).GetComponent<ChasingEnemy>();
                    enemyScripts.transform.localScale = Vector3.one;
                    enemyScripts.gameObject.SetActive(false);
                    enemyPools.Add(enemyScripts);
                }
            }
            enemyPools.Remove(enemyScripts);
            enemyScripts.gameObject.SetActive(true);
            return enemyScripts;

        }

        public void PushEnemy(ChasingEnemy enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.OnDespawn();
            enemyPools.Add(enemy);          
        }
        //private void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Vector3 mousePos = GridUtilities.GetMouseWorldPosition();
        //        Agent.SetDestination(mousePos);
        //    }
        //}

    }
}