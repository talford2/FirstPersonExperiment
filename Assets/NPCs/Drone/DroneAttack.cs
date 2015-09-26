using UnityEngine;
using System.Collections;

public class DroneAttack : BaseState<Drone>
{
	public float ShootTime = 0.4f;
	private float shootCooldown = 0;

	public DroneAttack(Drone npc) : base(npc)
	{
		Debug.Log("Attack");
	}

	public override void Update()
	{
		NPC.transform.LookAt(NPC.Target);

		if (NPC.SqrTargetDistance > NPC.SqrAttackStopRadius)
		{
			NPC.transform.position += NPC.transform.forward * NPC.AttackCruiseSpeed * Time.deltaTime;
		}

		if (NPC.SqrTargetDistance > NPC.SqrAttackRadius)
		{
			NPC.State = new DroneChase(NPC);
		}

		shootCooldown -= Time.deltaTime;
		if (shootCooldown <= 0)
		{
			shootCooldown = ShootTime;
			NPC.Muzzle.Flash();
			NPC.ShootSound.Play();
		}

		base.Update();
	}
}