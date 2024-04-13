using Godot;
using System;

public partial class ProjectileMicrowave : AnimatableBody2D {

	[Export] private float speed;

	public Vector2 Direction { get; set; }
	public ProjectilePool Pool { get; set; }

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		KinematicCollision2D collision = MoveAndCollide(Direction * speed);
		if (collision != null) {
			Pool.Despawn(this);

			if (collision.GetCollider() is Damageable damageable) {
				damageable.OnDamageReceived(0.1f);
			}
		}

	}

}
