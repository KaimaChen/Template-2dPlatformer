﻿using UnityEngine;

public class PlayerController : RaycastController
{
    [SerializeField]
    float m_speed = 6f;

    float m_groundAcceration = 0.1f;
    float m_airAcceration = 0.2f;

    float m_velocityXSmooth;
    Vector2 m_velocity;

    readonly JumpAbility m_jumpAbility = new JumpAbility();
    readonly ClimbWallAbility m_climbWallAbility = new ClimbWallAbility();

    #region get-set
    public Vector2 Velocity
    {
        get { return m_velocity; }
        set { m_velocity = value; }
    }
    #endregion

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(horizontal, vertical);
        CalcVelocityByInput(input);

        m_jumpAbility.Update(this, input);
        m_climbWallAbility.Update(this, input);
        
        Move(m_velocity * Time.deltaTime);

        //上或下碰到障碍时，重置竖直方向速度
        if(m_collisionInfo.m_above || m_collisionInfo.m_below)
        {
            m_velocity.y = 0;
        }
    }

    void CalcVelocityByInput(Vector2 input)
    {
        float targetX = input.x * m_speed;
        float acceration = (m_collisionInfo.m_below ? m_groundAcceration : m_airAcceration);
        m_velocity.x = Mathf.SmoothDamp(m_velocity.x, targetX, ref m_velocityXSmooth, acceration);

        m_velocity.y += Defines.c_gravity * Time.deltaTime;
    }
}
