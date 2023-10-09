using Godot;

public partial class UpgradeButton : Control
{
    private ShaderMaterial shaderMaterial;
    private Color originalColor;
    [Export] private Color changeColorOfBorder;
	private Color currentChangeColor;
	private Color currentColor;
    private TextureButton textureButton;

    private float transitionDuration = 0.1f; // Duration of the color transition in seconds
    private float elapsedTime = 0f; // Time elapsed during the transition

    public override void _Ready()
    {
		elapsedTime = transitionDuration;
        textureButton = GetParent().GetNode<TextureButton>("TextureButton");
        shaderMaterial = (ShaderMaterial)textureButton.Material;

        originalColor = (Color)shaderMaterial.GetShaderParameter("color");
    }

    private void _on_mouse_entered()
    {
        GD.Print("Enter");
        if(elapsedTime >= transitionDuration)
        	elapsedTime = 0f;

        currentChangeColor = changeColorOfBorder;
		currentColor = originalColor;

    }

    private void _on_mouse_exited()
    {
        GD.Print("Exit");
		if(elapsedTime >= transitionDuration)
        	elapsedTime = 0f;
        currentChangeColor = originalColor;
		currentColor = changeColorOfBorder;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
		UpdateShaderColor(delta);
    }

    private void UpdateShaderColor(double delta)
    {
        if (elapsedTime < transitionDuration)
        {
			GD.Print("Changing");
            float t = elapsedTime / transitionDuration;
            shaderMaterial.SetShaderParameter("color", currentColor.Lerp(currentChangeColor, t));
            elapsedTime += (float)delta;
        }
    }
}
