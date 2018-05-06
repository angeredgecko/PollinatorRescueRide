using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampParticles : MonoBehaviour {

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        particles= new ParticleSystem.Particle[ps.main.maxParticles];
    }
	
	// Update is called once per frame
	void Update () {
		if (ps.isPlaying)
        {
            int numParticlesAlive = ps.GetParticles(particles);

            // Change only the particles that are alive
            for (int i = 0; i < numParticlesAlive; i++)
            {
                particles[i].position = new Vector3(particles[i].position.x, particles[i].position.y, 0f);
            }
            ps.SetParticles(particles, numParticlesAlive);
        }
	}
}
