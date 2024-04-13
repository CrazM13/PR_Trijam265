using Godot;
using System;

public partial class EnemyMicrowaveEmitter : Node2D {

	[Export] private float movementSpeed;
	[Export] private float fireRate;

	[Export] private MicrowaveManager microwaveManager;

	private float timeUntilFire;

	public override void _Ready() {
		base._Ready();

		timeUntilFire = fireRate;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		timeUntilFire -= (float) delta;
		if (timeUntilFire <= 0) {
			microwaveManager.Fire(this.GlobalPosition, this.Transform.X);
			timeUntilFire += fireRate;
		}
	}

	public void UpdateTargeting(Node2D target) {
		Vector2 targetPos = target.GlobalPosition;
		
		float currentRotation = GlobalRotation;
		this.LookAt(targetPos);
		float targetRotation = GlobalRotation;

		GlobalRotation = Mathf.LerpAngle(currentRotation, targetRotation, movementSpeed);
	}

}
