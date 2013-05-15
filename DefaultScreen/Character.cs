namespace DefaultScreen
{
    internal class Character
    {
        public ushort backColor { get; set; }
        public ushort color { get; set; }
        public bool blinking { get; set; }

        public ushort id { get; set; }

        public Character(ushort id, ushort backColor, ushort color, bool blinking)
        {
            this.backColor = backColor;
            this.color = color;
            this.blinking = blinking;
            this.id = id;
        }

        public Character(ushort data)
        {
            color = (ushort) ((data >> 12) & 0xF);
            backColor = (ushort) ((data >> 8) & 0xF);
            blinking = ((data >> 7) & 0x1) == 1;
            id = (ushort) (data & 0x7F);
        }
    }
}