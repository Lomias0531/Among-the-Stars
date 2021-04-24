﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSystem
{
    public string Name { get; set; }
    public List<BasePlanet> planets { get; set; }
    public int planetCount { get; set; }
    public string systemType { get; set; }
    public float coordX { get; set; }
    public float coordY { get; set; }
    public float coordZ { get; set; }
    public float StarEnergy { get; set; }
    public GameObject star;

    public void Init()
    {
        Name = Tools.GetRandomName(4, 8);
        Init_MovePos();
        star = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Star"));
        star.transform.position = new Vector3(coordX*10, coordY*10, coordZ*10);
        star.name = Name;
        planetCount = Random.Range(1, 15);
        planets = new List<BasePlanet>();
        for(int i = 0;i<planetCount;i++)
        {
            BasePlanet planet = new BasePlanet();
            planet.Init(Name,i);
            planets.Add(planet);
        }
    }
    public void Init_MovePos()
    {
        for(int i = 0;i<5;i++)
        {
            coordX += Random.Range(-0.1f, 0.1f);
            coordY += Random.Range(-0.005f, 0.005f);
            coordZ += Random.Range(-0.1f, 0.1f);
        }
    }
    int GetPlanetType()
    {
        int planetType = 0;
        return planetType;
    }
}
