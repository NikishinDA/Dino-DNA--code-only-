using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private Chunk[] firstPrefab;
    [SerializeField] private Chunk finalPrefab;
    [SerializeField] private float spawnDistance;
    [SerializeField] private int concurrentChunkNumber;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private LevelTemplate[] templates;
    private LevelTemplate _template;
    private List<Chunk> _spawnedChunks;
    private bool _finishSpawned;
    private int _currentLength;
    private int _level;
    private bool _finished;
    [SerializeField] private float despawnDistance;

    private void Awake()
    {
        _spawnedChunks = new List<Chunk>();
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<PlayerFinishReachedEvent>(OnFinisherStart);
       _level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue) - 1;
        if (_level >= templates.Length)
        {
            _level = Random.Range(0, templates.Length);
        }
        _template = templates[_level % templates.Length];//tbc
        //VarSaver.LevelLength = _template.chunks.Length + firstPrefab.Length;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<PlayerFinishReachedEvent>(OnFinisherStart);

    }

    private void OnFinisherStart(PlayerFinishReachedEvent obj)
    {
        _finished = true;
    }

    private void OnGameStart(GameStartEvent obj)
    {
    }

    private void Start()
    {
        _currentLength = 0;
        foreach (Chunk ch in firstPrefab)
        {
            //ch.SetEnv(_level);
            _spawnedChunks.Add(ch);
        }
    }

    private void Update()
    {
        if ((!_finishSpawned) &&
            (playerTransform.position.z >
             _spawnedChunks[_spawnedChunks.Count - 1].end.position.z - spawnDistance))
        {
            SpawnChunk();
        }

        if (_spawnedChunks.Count > 1 && playerTransform.position.z > _spawnedChunks[0].end.position.z + despawnDistance)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private void SpawnChunk()
    {
        Chunk newChunk;
        if (_currentLength < _template.chunks.Length)
        {
            newChunk = Instantiate(chunkPrefabs[(int) _template.chunks[_currentLength].chunkType], chunkParent);
            newChunk.transform.position =
                _spawnedChunks[_spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
            _spawnedChunks.Add(newChunk);
            newChunk.InitializeChunk(_template.chunks[_currentLength]);
            //newChunk.SetEnv(_level);
        }
        else
        {
            newChunk = Instantiate(finalPrefab, chunkParent);
            newChunk.transform.position =
                _spawnedChunks[_spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
            _spawnedChunks.Add(newChunk);
            _finishSpawned = true;
        }


        _currentLength++;
        /*if (_spawnedChunks.Count > concurrentChunkNumber)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }*/
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _finishSpawned = true;
    }
}