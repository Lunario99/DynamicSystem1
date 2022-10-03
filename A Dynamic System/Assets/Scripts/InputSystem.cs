using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public bool changeColor = false;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private Material particlesMat;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        particles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(Reset());
        }

        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            position = Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0f, 0f, 3f));
            particles.Play(true);
            particles.transform.position = position;
            ChangeColor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            particles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
        
    }

    IEnumerator Reset()
    {
        changeColor = true;

        yield return new WaitForSeconds(0.1f);

        changeColor = false;
    }

    void ChangeColor()
    {
        particlesMat.color = new Color(Random.value, Random.value, Random.value, 1);
        particlesMat.SetColor("_EmissionColor", particlesMat.color);
    }
}
