
namespace TerrariaProtocol.DataStructures
{
    public class PlayerInfo
    {
        public byte SkinVariant, Hair, HairDye, HideVis1, HideVis2, HideMisc;
        public string Name;
        public Color HairColor, SkinColor, EyeColor, ShirtColor, UndershirtColor, PantsColor, ShoeColor;
        public Difficulty difficulty;

        public PlayerInfo()
        {
            SkinVariant = 0x00;
            Hair = 0x00;
            HairDye = 0x00;
            HideVis1 = 0x00;
            HideVis2 = 0x00;
            HideMisc = 0x00;
            HairColor = Color.RandomColor();
            SkinColor = Color.RandomColor();
            EyeColor = Color.RandomColor();
            ShirtColor = Color.RandomColor();
            UndershirtColor = Color.RandomColor();
            PantsColor = Color.RandomColor();
            ShoeColor = Color.RandomColor();
            difficulty = Difficulty.Normal | Difficulty.ExtraAcc;
            Name = "DummyPlayer";
        }
    }
}
