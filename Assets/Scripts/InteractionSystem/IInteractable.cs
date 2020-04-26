public interface IInteractable
{

    // So lange die Interaktionstaste gedrückt halten um zu interagieren (Sekunden)
    float HoldDuration { get; }

    // Soll die Interaktionstaste gehalten werden um zu interagieren?
    bool HoldToInteract { get; }

    // Kann das Objekt öfter verwendet werden (z.B. Lichtschalter)
    bool MultipleUse { get; }

    // Ist das Objekt gerade interagierbar?
    bool IsInteractable { get; }

    /**
     * Aufgerufen wenn Interaktion getriggert wird
     */
    void OnInteract();
}

