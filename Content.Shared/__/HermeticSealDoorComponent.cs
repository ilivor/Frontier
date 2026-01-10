using Robust.Shared.GameStates;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Content.Shared.Doors.Components;
using Content.Shared.Prying.Components;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared.Doors.Components;

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
