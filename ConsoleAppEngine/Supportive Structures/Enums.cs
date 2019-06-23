using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEngine.AllEnums
{
    public enum TextBookType
    {
        TextBook,
        Reference,
        Extra
    }

    public enum BranchType : byte
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
        Quiz,
        Assignment,
        Midsem,
        Comprehensive_Examination
    }
}