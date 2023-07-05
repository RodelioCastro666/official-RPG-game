using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Npc
{

    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private float initAggroRange;

    [SerializeField]
    private LootTable lootTable;

    public Vector3 MyStartPosition { get; set; }

    public float MyAggroRange { get; set;  }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }
    

    private IState currentState;

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }

            currentState.Update();

           
        }
        base.Update();

    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;

        return base.Select();
    }

    public override void DeSelect()
    {
        healthGroup.alpha = 0;

        base.DeSelect();
    }

    public override void TakeDamage(float damage, Transform source)
    {

        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);

            OnHealthChanged(health.MyCurrentValue);
        }
       
    }

    protected void Awake()
    {
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        MyAttackRange = 0.5f;
        ChangeState(new IdleState());
    }


    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void SetTarget(Transform source)
    {
        if(MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, source.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = source;
        }
    }

    public void Reset()
    {
        this.MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }

    public override void Interact()
    {
        if (!IsAlive)
        {
            lootTable.ShowLoot();
        }

    }
}
