namespace ConsoleAppEngine.AllEnums
{
    public enum BranchId
    {
        A1,
        A2,
        A3,
        A4,
        A5,
        A7,
        A8,

        AB,

        B1,
        B2,
        B3,
        B4,
        B5,

        PS
    }

    public enum ExpandedBranch
    {
        // AX
        ChemicalEngineering,
        CivilEngineering,
        ElectricalAndElectronics,
        Mechanical,
        BPharm,
        ComputerScience,
        ElectronicsAndInstrumentation,
        // AB
        ManufacturingEngineering,
        // BX
        Biology,
        Chemistry,
        Economics,
        Mathematics,
        Physics,

        // XX
        PS
    }

    public enum TextBookType
    {
        TextBook,
        Reference,
        Extra
    }

    public enum CourseType : byte
    {
        BIO,
        BIOT,
        BITS,
        CE,
        CHE,
        CHEM,
        CS,
        DE,
        ECON,
        EEE,
        FIN,
        GS,
        HSS,
        INSTR,
        IS,
        ITEB,
        MATH,
        MBA,
        ME,
        MEL,
        MF,
        MGTS,
        MSE,
        MUSIC,
        PHA,
        PHY,
        SS
    }

    public enum TestType
    {
        Tutorial_Test,
        Lab,
        Quiz,
        Assignment,
        Midsem,
        Comprehensive_Examination
    }

    public enum TimeTableEntryType
    {
        Lecture,
        Practical,
        Tutorial,
        CommonHour
    }
}