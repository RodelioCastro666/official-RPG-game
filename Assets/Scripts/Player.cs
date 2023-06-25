using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }


    [SerializeField]
    private Stat mana;

  

    [SerializeField]
    private Transform[] exitPoints;

    [SerializeField]
    private Blocks[] blocks;

    

    private int exitIndex = 2;

    private Vector3 max, min;
    

    private float initMana = 50;

   

    protected override void Start()
    {

        


        mana.Initialize(initMana, initMana);

       

        base.Start();

        
    }

    protected override void Update()
    {
        GetInput();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y),transform.position.z);

        base.Update();

    }

    public void SetLimits(Vector3 min , Vector3 max)
    {
        this.min = min;
        this.max = max;
    }

    public void GetInput()
    {
        Direction = Vector2.zero;
        

        

        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UPB"]))
        {
            exitIndex = 0;
            Direction += Vector2.up;
            
        }
           

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWNB"]))
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }
            

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFTB"]))
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }
           

        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHTB"]))
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }

        if (IsMoving)
        {
            StopAttack();
        }

        foreach (string action in KeybindManager.MyInstance.Actionbinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.Actionbinds[action]))
            {
                UiManager.MyInstance.ClickActionButton(action);
            }
        }
        
            
    }

    private IEnumerator Attack(string spellName)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);
           
        IsAttacking = true;

        MyAnimator.SetBool("attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }

           

        StopAttack();
        
        
    }

    
    public void CastSpell(string spellName)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellName));
        }

      
    }
    
    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        }
      

        return false;
    }

    private void Block()
    {
        foreach (Blocks b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }

   

    public  void StopAttack()
    {
        SpellBook.MyInstance.StopCasting();

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            IsAttacking = false;
            MyAnimator.SetBool("attack", IsAttacking);
        }

    }


}
