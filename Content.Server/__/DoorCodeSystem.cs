public sealed class DoorCodeSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();
        
        SubscribeLocalEvent<DoorCodeComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<DoorCodeComponent, ActivatableUIOpenAttemptEvent>(OnUIOpenAttempt);
        SubscribeLocalEvent<DoorCodeComponent, DoorCodeInputMessage>(OnInputMessage);
        SubscribeLocalEvent<DoorCodeComponent, DoorCodeSubmitMessage>(OnSubmitMessage);
        SubscribeLocalEvent<DoorCodeComponent, DoorCodeClearMessage>(OnClearMessage);
    }

    private void OnInit(EntityUid uid, DoorCodeComponent component, ComponentInit args)
    {
        // Генерируем случайный код если не установлен
        if (string.IsNullOrEmpty(component.Code))
        {
            component.Code = GenerateRandomCode(component.CodeLength);
        }
    }

    private void OnInputMessage(EntityUid uid, DoorCodeComponent component, DoorCodeInputMessage args)
    {
        // Добавляем цифру к введенному коду (если есть место)
        if (component.EnteredCode.Length < component.CodeLength)
        {
            component.EnteredCode += args.Digit.ToString();
            UpdateUserInterface(uid, component);
        }
    }

    private void OnSubmitMessage(EntityUid uid, DoorCodeComponent component, DoorCodeSubmitMessage args)
    {
        // Проверяем код
        if (component.EnteredCode == component.Code)
        {
            // Код верный - разблокируем дверь
            component.IsLocked = false;
            component.Status = DoorCodeStatus.CodeCorrect;
            
            // Разблокируем дверь
            if (TryComp<DoorComponent>(uid, out var door))
            {
                // Логика разблокировки двери
            }
        }
        else
        {
            // Код неверный
            component.Status = DoorCodeStatus.CodeIncorrect;
            component.EnteredCode = ""; // Сбрасываем ввод
        }
        
        UpdateUserInterface(uid, component);
    }

    private void OnClearMessage(EntityUid uid, DoorCodeComponent component, DoorCodeClearMessage args)
    {
        component.EnteredCode = "";
        UpdateUserInterface(uid, component);
    }

    private string GenerateRandomCode(int length)
    {
        var random = new Random();
        var code = "";
        for (int i = 0; i < length; i++)
        {
            code += random.Next(0, 10).ToString();
        }
        return code;
    }
}