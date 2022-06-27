//Make Random object
Random DiceRoll = new Random();

//Build array
Console.WriteLine("This is a test run of a map tag generator");
int UserSelection;
do
{
    Console.WriteLine("Please select a gameboard size:");
    Console.WriteLine(" 1 - Small Map (4x4)");
    Console.WriteLine(" 2 - Medium Map (6x6)");
    Console.WriteLine(" 3 - Large Map (8x8)");
    UserSelection = Convert.ToInt16(Console.ReadLine());
    Console.Clear();
} while (UserSelection < 0 && UserSelection < 3);

Rooms[,] TagRooms = UserSelection switch
{
    1 => new Rooms[4, 4],
    2 => new Rooms[6, 6],
    3 => new Rooms[8, 8],
};

// Start of auto deploy
RoomTagger(TagRooms, Rooms.exit, DiceRoll);
RoomTagger(TagRooms, Rooms.foutain, DiceRoll);
RoomTagger(TagRooms, Rooms.pit, DiceRoll);
RoomTagger(TagRooms, Rooms.maelstrom, DiceRoll);
RoomTagger(TagRooms, Rooms.amarok, DiceRoll);

Console.WriteLine("This is the end of the program");
Console.WriteLine("Put debug break point at line 33 (ReadKey statement) to see TagRooms array results");
Console.ReadKey(); //Break point to check the array

void RoomTagger(Rooms[,] tagRooms, Rooms RoomType, Random Diceroll)
{
    if (RoomType == Rooms.exit)
    {
        int row = 0;
        int col = 0;
        TagRooms[row, col] = Rooms.exit;
    }
    else if (RoomType == Rooms.foutain)
    {
        int row = 0;
        int col = 0;
        bool GoodTarget = false;
        (int RowRng1, int RowRng2, int ColRng1, int ColRng2, int Multiple) Settings = SetBoardRnds(tagRooms, RoomType);

        do
        {
            row = Diceroll.Next(Settings.RowRng1, Settings.RowRng2);
            col = Diceroll.Next(Settings.ColRng1, Settings.ColRng2);
            if (TagRooms[row, col] == Rooms.regular) GoodTarget = true;
        } while (GoodTarget == false);        
        TagRooms[row, col] = Rooms.foutain;
    }
    else if (RoomType == Rooms.pit)
    {
        int row = 0;
        int col = 0;
        bool GoodTarget = false;
        (int RowRng1, int RowRng2, int ColRng1, int ColRng2, int Multiple) Settings = SetBoardRnds(tagRooms, RoomType);
        for (int i = 0; i < Settings.Multiple; i++)
        {
            do
            {
                row = Diceroll.Next(Settings.RowRng1, Settings.RowRng2);
                col = Diceroll.Next(Settings.ColRng1, Settings.ColRng2);
                if (TagRooms[row, col] == Rooms.regular) GoodTarget = true;
            } while (GoodTarget == false);
            TagRooms[row, col] = Rooms.pit;
        }
    }
    else if (RoomType == Rooms.maelstrom)
    {
        int row = 0;
        int col = 0;
        bool GoodTarget = false;
        (int RowRng1, int RowRng2, int ColRng1, int ColRng2, int Multiple) Settings = SetBoardRnds(tagRooms, RoomType);
        for (int i = 0; i < Settings.Multiple; i++)
        {
            do
            {
                row = Diceroll.Next(Settings.RowRng1, Settings.RowRng2);
                col = Diceroll.Next(Settings.ColRng1, Settings.ColRng2);
                if (TagRooms[row, col] == Rooms.regular) GoodTarget = true;
            } while (GoodTarget == false);
            TagRooms[row, col] = Rooms.maelstrom;
        }
    }
    else if (RoomType == Rooms.amarok)
    {
        int row = 0;
        int col = 0;
        bool GoodTarget = false;
        (int RowRng1, int RowRng2, int ColRng1, int ColRng2, int Multiple) Settings = SetBoardRnds(tagRooms, RoomType);
        for (int i = 0; i < Settings.Multiple; i++)
        {
            do
            {
                row = Diceroll.Next(Settings.RowRng1, Settings.RowRng2);
                col = Diceroll.Next(Settings.ColRng1, Settings.ColRng2);
                if (TagRooms[row, col] == Rooms.regular) GoodTarget = true;
            } while (GoodTarget == false);

            TagRooms[row, col] = Rooms.amarok;
        }
    }
    else {/*not needed at this time*/ }
}

//set number of items in map based on room type
(int, int, int, int, int) SetBoardRnds(Rooms[,] tagRooms, Rooms RoomType)
{
    (int row1, int row2, int col1, int col2, int Multiple) targetrnds;

    if (TagRooms.Length == 16)
    {
        if (RoomType == Rooms.foutain) return targetrnds = (1, 2, 2, 3, 1);
        else if (RoomType == Rooms.pit) return targetrnds = (0, 3, 0, 3, 1);
        else if (RoomType == Rooms.maelstrom) return targetrnds = (0, 3, 0, 3, 1);
        else return targetrnds = (0, 3, 0, 3, 1); //amarok
    }
    else if (TagRooms.Length == 36)
    {
        if (RoomType == Rooms.foutain) return targetrnds = (1, 4, 2, 4, 1);
        else if (RoomType == Rooms.pit) return targetrnds = (0, 5, 0, 5, 2);
        else if (RoomType == Rooms.maelstrom) return targetrnds = (0, 5, 0, 5, 1);
        else return targetrnds = (0, 5, 0, 5, 2); //amarok
    }
    else if (TagRooms.Length == 64)
    {
        if (RoomType == Rooms.foutain) return targetrnds = (1, 6, 2, 6, 1);
        else if (RoomType == Rooms.pit) return targetrnds = (0, 7, 0, 7, 4);
        else if (RoomType == Rooms.maelstrom) return targetrnds = (0, 7, 0, 7, 2);
        else return targetrnds = (0, 7, 0, 7, 3); //amarok
    }
    else return targetrnds = (0, 0, 0, 0, 0); //FailSafe
}

//Enum
public enum Rooms { regular, foutain, exit, pit, maelstrom, amarok }

