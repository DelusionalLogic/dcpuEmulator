namespace DefaultScreen
{
    /// <summary>
    /// A single charater in memory
    /// </summary>
    internal class Character
    {
        /// <summary>
        /// Gets or sets the color of the back
        /// </summary>
        /// <value>
        /// The color of the back
        /// </value>
        public ushort backColor { get; set; }

        /// <summary>
        /// Gets or sets the color
        /// </summary>
        /// <value>
        /// The color
        /// </value>
        public ushort color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Character"/> is blinking
        /// </summary>
        /// <value>
        ///   <c>true</c> if blinking; otherwise, <c>false</c>.
        /// </value>
        public bool blinking { get; set; }

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        /// <value>
        /// The id
        /// </value>
        public ushort id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="backColor">Color of the back</param>
        /// <param name="color">The color</param>
        /// <param name="blinking">if set to <c>true</c> [blinking].</param>
        public Character(ushort id, ushort backColor, ushort color, bool blinking)
        {
            this.backColor = backColor;
            this.color = color;
            this.blinking = blinking;
            this.id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class
        /// </summary>
        /// <param name="data">The hexidecimal data</param>
        public Character(ushort data)
        {
            color = (ushort) ((data >> 12) & 0xF);
            backColor = (ushort) ((data >> 8) & 0xF);
            blinking = ((data >> 7) & 0x1) == 1;
            id = (ushort) (data & 0x7F);
        }
    }
}