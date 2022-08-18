using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FogOnHex : MonoBehaviour
{
    public ParticleSystem ParticleSystem; // wordt geset door ander script bij het aanmaken van dit script

    private void Start()
    {
        this.SetFog(false);
    }

    public void SetFog(bool isFogActive)
    {
        ParticleSystem.gameObject.SetActive(isFogActive);
    }

    public bool FogIsActive() => ParticleSystem.gameObject.activeSelf;
}