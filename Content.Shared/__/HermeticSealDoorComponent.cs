[RegisterComponent, NetworkedComponent]
public sealed partial class HermeticCodePaperComponent : Component
{
    [DataField]
    public string GeneratedCode = "";
    
    [DataField]
    public EntityUid LinkedSeal = EntityUid.Invalid;
    
    [DataField]
    public TimeSpan GenerationTime; // Когда сгенерирован код
}