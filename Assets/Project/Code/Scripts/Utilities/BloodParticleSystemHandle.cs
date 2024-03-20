using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleSystemHandle : MonoBehaviour
{
    public static BloodParticleSystemHandle Instance { get; private set; }

    private MeshParticleSystem meshParticleSystem;
    private List<Single> singleList;

    private void Awake()
    {
        Instance = this;
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        singleList = new List<Single>();
    }

    private void Update()
    {
        for (int i = 0; i < singleList.Count; i++)
        {
            Single single = singleList[i];
            single.Update();
            if (single.IsParticleComplete())
            {
                singleList.RemoveAt(i);
                i--;
            }
        }
    }

    public void SpawnBlood(Vector3 position, Vector3 direction)
    {
        float bloodParticleCount = 5;
        for (int i = 0; i < bloodParticleCount; i++)
        {
            singleList.Add(new Single(position, Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction, meshParticleSystem));
        }
    }

    private class Single
    {

        private MeshParticleSystem meshParticleSystem;
        private Vector3 position;
        private Vector3 direction;
        private int quadIndex;
        private Vector3 quadSize;
        private float moveSpeed;
        private float rotation;
        private int uvIndex;

        public Single(Vector3 position, Vector3 direction, MeshParticleSystem meshParticleSystem)
        {
            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = meshParticleSystem;

            quadSize = new Vector3(0.5f, 0.5f);
            rotation = Random.Range(0, 90f);
            moveSpeed = Random.Range(2f, 15f);
            uvIndex = Random.Range(0, 8);

            quadIndex = meshParticleSystem.AddQuad(position, rotation, quadSize, false, uvIndex);
        }

        public void Update()
        {
            position += direction * moveSpeed * Time.deltaTime;
            rotation += 360f * (moveSpeed / 5f) * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, rotation, quadSize, false, uvIndex);

            float slowDownFactor = 3f;
            moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime;
        }
        public bool IsParticleComplete()
        {
            return moveSpeed < .1f;
        }
    }
}
