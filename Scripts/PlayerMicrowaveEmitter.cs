using Godot;
using System;

public partial class PlayerMicrowaveEmitter : Node2D {

	[Export] private MicrowaveManager microwaveManager;

	private bool isMousePressed = false;

	public override void _Process(double delta) {
		base._Process(delta);

		Vector2 mousePosition = GetViewport().GetMousePosition();

		LookAt(mousePosition);

		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			if (!isMousePressed) {
				microwaveManager.Fire(this.GlobalPosition, this.Transform.X);
				isMousePressed = true;
			}
		} else {
			isMousePressed = false;
		}
	}

}
