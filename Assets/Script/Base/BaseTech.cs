using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTech
{
    //科技名称
    public string TechName;
    //前置科技
    public List<BaseTech> RequiredTech = new List<BaseTech>();
    //是否解锁
    public bool isUnlocked = false;
    //需求点数
    public int PointsRequired;
    //已投入点数
    public int PointConsumed = 0;
    //是否满足解锁条件
    public bool RequirementFulfilled
    {
        get
        {
            bool req = true;
            if(RequiredTech.Count>0)
            {
                foreach (var tec in RequiredTech)
                {
                    if (!tec.isUnlocked)
                    {
                        req = false;
                    }
                }
            }
            return req;
        }
    }
}
