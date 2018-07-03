using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour {
	private Rigidbody2D rigidbody;
	public float jumpPower= 20f;
	public float runSpeed = 2f;
	private float direction = 1;
	public Animator animator;
	public AnimationEvent aniEvent;

	public RectTransform rect;
	private bool attack;
	private bool dash;
	private bool jump;
	private float dashTime;
	public bool deth;
	private bool skill;
	private bool jumpSkill;
	
	private Collider2D attackRange;
	
	// Use this for initialization
	void Start () {
		attackRange = transform.GetChild(0).GetComponent<Collider2D>();
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat("rigidY", rigidbody.velocity.y);
		attackRange.enabled = false;
		if(deth)
			return;
		if(attack||skill)
		{

		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			direction = 1f;
			if(!dash)
			rect.anchoredPosition += new Vector2(direction*runSpeed*Time.deltaTime,0f);
			rect.eulerAngles = Vector3.zero;
			animator.SetBool("run", true);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			direction = -1f;
			if(!dash)
			rect.anchoredPosition += new Vector2(direction*runSpeed*Time.deltaTime,0f);
			animator.SetBool("run", true);
			rect.eulerAngles = new Vector3(0f, 180f, 0f);
		}
		else	
		{
			animator.SetBool("run", false);
		}

		if(Input.GetKey(KeyCode.DownArrow))
		{
			
		}


		if(Input.GetKeyDown(KeyCode.X)&& rigidbody.velocity.y == 0f)
		{
			rigidbody.velocity += new Vector2(0f, jumpPower);
		}
		if(Input.GetKeyDown(KeyCode.C))
		{
			if(rigidbody.velocity.y == 0f)
			{
				attack = true;
				animator.SetBool("atk",attack);
			}
			else
			{
				animator.SetBool("jumpAtk", true);
			}
			
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			if(rigidbody.velocity.y == 0f)
			{
				skill = true;
				animator.SetBool("skill",true);
			}
			else
			{
				animator.SetBool("jumpSkill", true);
			}
			
		}
		
		if(Input.GetKeyDown(KeyCode.Z)&&rigidbody.velocity.y==0f)
		{
			dash = true;
			animator.SetBool("dash", true);
		}
		if(dash)
		{
			if(rigidbody.velocity.y==0f)
			{
				if(dashTime <0.3f)
				{
					rect.anchoredPosition += new Vector2(direction*runSpeed*Time.deltaTime * 2f,0f);
					dashTime += Time.deltaTime;
				}
				else
				{
					dash = false;
					animator.SetBool("dash", false);
				}
			}
			else
			{
				rect.anchoredPosition += new Vector2(direction*runSpeed*Time.deltaTime * 2f,0f);
				dashTime += Time.deltaTime;
			}

		}
		else
		{
			dashTime=0f;
			animator.SetBool("dash", false);
		}
	}
	public void AtkEnd()
	{
		attack = false;
		skill = false; 
		animator.SetBool("jumpAtk", false);
		animator.SetBool("atk",attack);
		animator.SetBool("skill",false);
		animator.SetBool("jumpSkill",false);
		attackRange.enabled = true;
	}
	public void Deth()
	{
		animator.Play("Ninja_deth",0);
		deth = true;
	}
}
