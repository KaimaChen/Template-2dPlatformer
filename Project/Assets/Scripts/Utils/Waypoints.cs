﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> m_Points = new List<Vector2>();

    public List<Vector2> Points { get { return m_Points; } }
    public int Count { get { return m_Points.Count; } }

    public Vector2 GetPoint(int index)
    {
        return m_Points[index];
    }
}
