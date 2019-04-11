using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class par : MonoBehaviour
{
    public ParticleSystem system;

    void Start()
    {
        // A simple particle material with no texture.
        //Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));

       
        var go = new GameObject("Particle System");
        go.transform.Rotate(-90, 0, 0); // Rotate so the system emits upwards.
        system = go.AddComponent<ParticleSystem>();
        //go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        var mainModule = system.main;
        

        
    }

    public void DoEmit()
    {
        Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));
        // Any parameters we assign in emitParams will override the current system's when we call Emit.
        // Here we will override the start color and size.
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.red;
        emitParams.startSize = 0.2f;
        system.Emit(emitParams, 10);
        system.Play(); // Continue normal emissions
    }
}