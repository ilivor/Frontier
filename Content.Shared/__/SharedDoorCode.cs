[RegisterComponent]
public sealed partial class DoorCodeComponent : Component
namespace Content.Shared._Forge.CodeDoor;
{
    [ViewVariables]
    public string Code = ""; // Правильный код

    [ViewVariables]
    public string EnteredCode = ""; // Введенный код

    [DataField("codeLength")]
    public int CodeLength = 4; // Длина кода

    [ViewVariables]
    public bool IsLocked = true; // Заблокирована ли дверь

    [ViewVariables]
    public DoorCodeStatus Status = DoorCodeStatus.AwaitingCode;
}
