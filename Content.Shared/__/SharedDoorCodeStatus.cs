using Content.Shared._Forge.CodeDoor;
namespace Content.Shared._ForgeCodeDoor
{
    public enum DoorCodeStatus : byte
    {
        AwaitingCode,   // Ожидание ввода
        CodeCorrect,    // Код верный
        CodeIncorrect   // Код неверный
    }

    public sealed class DoorCodeUiState : BoundUserInterfaceState
    {
        public DoorCodeStatus Status { get; }
        public string EnteredCode { get; }
        public int CodeLength { get; }

        public DoorCodeUiState(DoorCodeStatus status, string enteredCode, int codeLength)
        {
            Status = status;
            EnteredCode = enteredCode;
            CodeLength = codeLength;
        }
    }

    public sealed class DoorCodeInputMessage : BoundUserInterfaceMessage
    {
        public int Digit { get; }
        public DoorCodeInputMessage(int digit) => Digit = digit;
    }

    public sealed class DoorCodeSubmitMessage : BoundUserInterfaceMessage { }
    public sealed class DoorCodeClearMessage : BoundUserInterfaceMessage { }
    public enum DoorCodeUiKey : byte
    {
        Key
    }
}
