using Content.Shared.Containers.ItemSlots;
using Content.Shared.DoorCode;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;

namespace Content.Client.DoorCode
{
    [UsedImplicitly]
public sealed class DoorCodeBoundUserInterface : BoundUserInterface
{
    private DoorCodeMenu? _menu;

    public DoorCodeBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey) {}

    protected override void Open()
    {
        base.Open();
        _menu = new DoorCodeMenu();
        
        _menu.OnKeypadButtonPressed += i => SendMessage(new DoorCodeInputMessage(i));
        _menu.OnClearButtonPressed += () => SendMessage(new DoorCodeClearMessage());
        _menu.OnEnterButtonPressed += () => SendMessage(new DoorCodeSubmitMessage());
    }
}