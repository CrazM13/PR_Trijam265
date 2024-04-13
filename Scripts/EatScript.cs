using Godot;
using System;

public partial class EatScript : RayCast2D {

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		GodotObject collider = this.GetCollider();
		if (collider is Building building) {
			if (building.CurrentState == Building.BuildingStates.COOKED) {
				building.OnDamageReceived(1000);
			}
		}

		if (collider is PedestrianAI pedestrian) {
			if (pedestrian.CurrentState == PedestrianAI.PedestrianStates.COOKED) {
				pedestrian.OnDamageReceived(1000);
			}
		}
	}

}
