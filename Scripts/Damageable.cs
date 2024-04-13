using Godot;
using System;

public partial class Damageable : CharacterBody2D {

	[Export] private float maxHealth;
	
	public float Health { get; set; }
	public float MaxHealth { get => maxHealth; }

	[Signal]
	public delegate void OnDamageEventHandler();

	public void OnDamageReceived(float damage) {
		Health -= damage;

		EmitSignal(SignalName.OnDamage, damage);
	}

}
