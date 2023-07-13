namespace Matflo.Common.Audio
{
    public enum AudioName
    {

        NotSet=-1,
        ButtonClick = 0,
        Correct = 1,
        Wrong = 2,
        Blank1 = 3,
        Blank2 = 4,
        Blank3 = 5,
        MatflowIntro1=6,
        MatflowIntro2 = 7,
        MatflowIntro3 = 8,

        // putaway
        OIScreen = 9,
        PickingLocation = 10,
        ScanTM = 11,
        ScanSKU = 12,
        PutawayLocation = 13,
        ScanLocation = 14,
        PutTM = 15,

        //Receiving
        OIScreenReceiving = 16,
        SelectDeliveryReceiving = 17,
        ScanSKUReceiving= 18,
        ScanTMReceiving= 19,
        EnterQuantityReceiving = 20,
        PutTMReceiving= 21,
        ClosingDeliveryReceiving = 22,
    }
}