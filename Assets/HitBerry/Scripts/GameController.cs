using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

   public class GameController : MonoBehaviour
    {
        #region Variables

        private static GameController s_instance;
        private GameObject _clientPrefab;

        [SerializeField] private LevelConfig[] _levels;

        private List<ObjectPooler> Pools { get; set; }
        public Color CurrentColor { get; private set; }
        public int CurrentLevel { get; set; }
        public int CurrentPercent { get; set; }
        public static GameController Instance => s_instance;

        #endregion

        #region Functions

        private void Awake() =>  s_instance = this;

        private void Start()
        {
            Pools = new List<ObjectPooler>();
            DataStore.LoadGame();
            StartLevel();
        }

        private void OnEnable()
        {
            EventController.OnLevelRestarted += LevelRestarted;
            EventController.OnNextLevel += NextLevel;
        }

        private void OnDisable()
        {
            EventController.OnLevelRestarted -= LevelRestarted;
            EventController.OnNextLevel -= NextLevel;
        }

        public void StartLevel()
        {
            CurrentColor = _levels[CurrentLevel - 1].OrderColor;
            _clientPrefab = Instantiate(_levels[CurrentLevel - 1].ClientPrefab);

            for (int i = 0; i < _levels[CurrentLevel - 1].FoodPrefabs.Length; i++)
            {
                Pools.Add(new ObjectPooler(_levels[CurrentLevel - 1].FoodPrefabs[i], 8));
                StartCoroutine(SpawnFood(i));
            }
        }

        public IEnumerator SpawnFood(int index)
        {
            yield return new WaitForSeconds(.4f);
            var food = Pools[index].GetPooledObject();
            var localScaleX = food.transform.localScale.x;
            var localScaleY = food.transform.localScale.y;
            var localScaleZ = food.transform.localScale.z;
            food.gameObject.SetActive(true);
            food.transform.position = _levels[CurrentLevel - 1].FoodPositions[index];
            food.transform.rotation = Quaternion.Euler(_levels[CurrentLevel - 1].FoodPrefabs[index].transform.localEulerAngles);
            food.transform.DOScale(new Vector3(localScaleX + .05f, localScaleY + .05f, localScaleZ + .05f), 
                .5f).OnComplete(() =>
            {
                food.transform.DOScale(
                    new Vector3(localScaleX - .05f, localScaleY - .05f, localScaleZ - .05f), .5f);
            });
            food.PoolIndex = index;
        }

        private void NextLevel()
        {
            if (CurrentLevel != 3)
            {
                CurrentLevel++;
                DataStore.SaveGame();
            }
            else
            {
                CurrentLevel = 1;
                DataStore.SaveGame();
            }
            LevelRestarted();
        }

        private void LevelRestarted()
        {
            foreach (var pool in Pools)
            {
                pool.DestroyPoolObjects();
            }
            Pools.Clear();
            CurrentPercent = 0;
            Destroy(_clientPrefab);
            StartLevel();
        }

        public void ChangeMedalPrice()
        {
            switch (CurrentPercent)
            {
                case < 90:
                    return;
                case >= 90 and < 95:
                    StartCoroutine(HUD.Instance.ShowMedal(2));
                    break;
                case >= 95 and < 98:
                    StartCoroutine(HUD.Instance.ShowMedal(1));
                    break;
                case >= 98:
                    StartCoroutine(HUD.Instance.ShowMedal(0));
                    break;
            }
        }
        #endregion
    }