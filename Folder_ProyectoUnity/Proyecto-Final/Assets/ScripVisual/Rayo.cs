using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{
    public Transform final;
    public int Cpuntos;
    public float dirpersion;
    public float frecuencia;

    private LineRenderer line;
    private float tiempo = 0;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        tiempo += Time.deltaTime;
        if(tiempo> frecuencia)
        {
            ActualizarPuntos(this.line);
            tiempo = 0;
        }
    }
    private void ActualizarPuntos(LineRenderer line)
    {
        List<Vector3> puntos = InterpolarPuntos(Vector3.zero, final.localPosition, Cpuntos);
        line.positionCount = puntos.Count;
        line.SetPositions(puntos.ToArray());
    }
    private List<Vector3> InterpolarPuntos(Vector3 principio, Vector3 final, int totalPoints)
    {
        List<Vector3> list = new List<Vector3>();
        for (int i = 0; i < totalPoints; ++i)
        {
            list.Add(Vector3.Lerp(principio, final, (float)i / totalPoints) + DesfaseAleatorio());
        }
        return list;
    }
    private Vector3 DesfaseAleatorio()
    {
        return Random.insideUnitSphere.normalized * Random.Range(0, dirpersion);
    }
}
