using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Vehicle : MonoBehaviour
{
    private float m_speed;
    private float m_acceleration;
    private float m_angularSpeed;
    private Stand m_target;
    private NavMeshAgent m_Agent;
    public bool busy { get; private set; }
    // ENCAPSULATION
    public float Speed
    {  get { return m_speed; } 
       set { if (value > 0.5f && value < 50f) m_speed = value; } 
    }
    // ENCAPSULATION
    public float Acceleration
    {
        get { return m_acceleration; }
        set { if (value > 1f && value < 500f) m_acceleration = value; }
    }
    // ENCAPSULATION
    public float AngularSpeed
    {
        get { return m_angularSpeed; }
        set { if (value > 1f && value < 500f) m_angularSpeed = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target != null)
        {
            float distance = Vector3.Distance(m_target.transform.position, transform.position);
            if (distance < 1.0f)
            {
                m_Agent.isStopped = true;
                ArrivedAtStation();
            }
        }
    }
    public void ArrivedAtStation()
    {
        busy = false;
        m_target = null;
    }


    protected virtual void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        SetNavmeshData();
    }
    // ABSTRACTION
    protected void SetNavmeshData()
    {
        m_Agent.speed = m_speed;
        m_Agent.acceleration = m_acceleration;
        m_Agent.angularSpeed = m_angularSpeed;
    }
    // ABSTRACTION
    public virtual void GoTo(Vector3 position)
    {
        //we don't have a target anymore if we order to go to a random point.
        //m_Target = null;
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;
        busy = true;
    }
    // ABSTRACTION
    public virtual void GoTo(Stand target)
    {
        m_target = target;

        if (m_target != null)
        {
            m_Agent.SetDestination(m_target.transform.position);
            m_Agent.isStopped = false;
            busy = true;
        }
    }

}
