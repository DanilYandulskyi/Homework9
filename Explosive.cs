using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorSeter))]
[RequireComponent(typeof(Rigidbody))]
public class Explosive : Spawner<Explosive>
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private int _spawnChance = 100;
   
    private List<Explosive> _clones = new List<Explosive>();
    private Rigidbody _rigidbody;
    private int _maxSpawningExplosiveCount = 6;
    private int _minSpawningExplosiveCount = 2;
    private int _dividedCoefficient = 2;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        Explode();
    }

    private void Explode()
    {
        foreach (Explosive explosive in _clones)
        {
            explosive._rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        if (ChanceGenerator.CalculateChances(_spawnChance))
            SpawnExplosive();

        Destroy(gameObject);
    }

    private void SpawnExplosive()
    {
        int spawningExplosiveCount = Random.Range(_minSpawningExplosiveCount, _maxSpawningExplosiveCount);
        
        for (int i = 0; i < spawningExplosiveCount; i++)
        {
            Explosive clone = Spawn();
            clone.OnSpawn();
            _clones.Add(clone);
        }
    }

    public void OnSpawn()
    {
        _spawnChance /= _dividedCoefficient;
        transform.localScale /= _dividedCoefficient;
    }
}