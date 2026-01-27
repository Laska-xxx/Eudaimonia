using System.Collections.Generic;
using UnityEngine;

public class BloodPoolController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> bloodParticles = new();

    private int _curIndex = 0;

    public ParticleSystem GetParticle()
    {
        if (_curIndex == bloodParticles.Count)
            _curIndex = 1;

        var particle = bloodParticles[_curIndex];
        _curIndex++;
        return particle;
    }
}
