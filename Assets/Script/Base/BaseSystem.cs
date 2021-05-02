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

    public void Init(StarType type)
    {
        Name = Tools.GetRandomName(4, 8);
        systemType = type.starName;
        Init_MovePos();
        star = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Star"));
        star.GetComponent<StarSysUI>().thisStarSystem = this;
        star.transform.position = new Vector3(coordX * 10, coordY * 10, coordZ * 10);
        star.name = Name;
        StarEnergy = Random.Range(type.starMinEnergy, type.starEnergy);
        planetCount = Random.Range(0, type.maxPlanets);
        planets = new List<BasePlanet>();
        float dis = 0;
        for (int i = 0; i < planetCount; i++)
        {
            BasePlanet planet = new BasePlanet();
            dis += Random.Range(0.5f, 1.5f);
            PlanetType planetType;
            float ene;
            do
            {
                planetType = Tools.RandomValues(Config.Instance.planetTypes);
                ene = Mathf.Clamp(StarEnergy - dis, 0, 120);
            } while (ene < planetType.minStarEnergy || ene > planetType.maxstarEnergy);
            planet.planetType = planetType.planetName;
            planet.Init(Name, i, dis,planetType);
            planets.Add(planet);
            if (dis > type.maxDistance)
            {
                break;
            }
        }
    }
    public void Init_MovePos()
    {
        for (int i = 0; i < 5; i++)
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
