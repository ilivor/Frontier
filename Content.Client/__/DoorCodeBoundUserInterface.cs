using Content.Shared.Containers.ItemSlots;
using Content.Shared._Forge.CodeDoor;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;
using Content.Shared._ForgeCodeDoor;

namespace Content.Client.__
{
    [UsedImplicitly]
    public sealed class DoorCodeBoundUserInterface : BoundUserInterface
    {
        private DoorCodeMenu? _menu;

        public DoorCodeBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey) { }

        protected override void Open()
        {
            base.Open();
            _menu = this.CreateWindow<DoorCodeMenu>();

            _menu.OnKeypadButtonPressed += i => SendMessage(new DoorCodeInputMessage(i));
            _menu.OnClearButtonPressed += () => SendMessage(new DoorCodeClearMessage());
            _menu.OnEnterButtonPressed += () => SendMessage(new DoorCodeSubmitMessage());
        }
    }
}
