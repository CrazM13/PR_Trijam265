using Godot;
using System;

public partial class ProjectileMicrowave : AnimatableBody2D {

	[Export] private float speed;

	public Vector2 Direction { get; set; }
	public ProjectilePool Pool { get; set; }

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		if (MoveAndCollide(Direction * speed) != null) {
			Pool.Despawn(this);
		}

	}

}
