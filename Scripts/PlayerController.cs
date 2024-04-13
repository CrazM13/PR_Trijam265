using Godot;
using System;

public partial class PlayerController : Damageable {

	[ExportCategory("References")]
	[Export] private SceneTransition sceneManager;
	[Export] private AudioManager audioManager;
	[ExportCategory("Settings")]
	[Export] private float Speed = 300.0f;
	[Export] private float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready() {
		base._Ready();

		this.Health = this.MaxHealth;

		this.OnDamage += ReactToDamage;
	}

	private void ReactToDamage() {
		if (Health <= 0) {
			sceneManager.ReloadScene();
		}
	}

	public override void _PhysicsProcess(double delta) {
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float) delta;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero) {
			velocity.X = direction.X * Speed;

			if (direction.Y < 0 && IsOnFloor()) {
				velocity.Y = JumpVelocity;
			}
		} else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (Input.IsActionJustPressed("ui_accept")) {
			//audioManager.PlayGlobal(0);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
