using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generator
{
    public class BlockGenerator : MonoBehaviour
    {
        private int _nGenerated = 1;
        private int _offset = -18;

        public List<GameObject> blocks;

        public void Awake()
        {
            PlayerController.PlayerStartBlockEvent += Spawn;
        }

        private void OnDestroy()
        {
            PlayerController.PlayerStartBlockEvent -= Spawn;
        }

        private void Spawn()
        {
            GameObject blockToSpawn = blocks[Random.Range(0, blocks.Count)];
            Instantiate(blockToSpawn, new Vector3(0, _nGenerated * _offset, 0), Quaternion.identity, transform);
            
            _nGenerated++;
        }
    }
}