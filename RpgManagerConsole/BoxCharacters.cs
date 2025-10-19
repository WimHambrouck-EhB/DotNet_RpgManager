namespace RpgManagerConsole
{
    /// <summary>
    /// Used by the <see cref="ConsoleHelper"/> class to represent a set of characters used to draw boxes in the console.
    /// </summary>
    /// <param name="Horizontal">The character used to represent horizontal lines.</param>
    /// <param name="Vertical">The character used to represent vertical lines.</param>
    /// <param name="UpperLeft">The character used for the upper-left corner of the box.</param>
    /// <param name="LowerLeft">The character used for the lower-left corner of the box.</param>
    /// <param name="UpperRight">The character used for the upper-right corner of the box.</param>
    /// <param name="LowerRight">The character used for the lower-right corner of the box.</param>
    internal readonly record struct BoxCharacters(char Horizontal, char Vertical, char UpperLeft, char LowerLeft, char UpperRight, char LowerRight);

    // Gebruik van records is buiten de scope van de cursus, maar wordt hier gebruikt om snel een alleen-lezen struct te definiëren met een constructor en properties.
    /*
     * Van links naar rechts:
     *   internal         = access modifier (zie slides)
     *   readonly         = immutable (properties kunnen niet aangepast worden na initialisatie)
     *   record           = compiler schrijft automatisch constructor, properties, Equals, GetHashCode en ToString aan
     *   struct           = value type (in tegenstelling tot een class, wat een reference type is)
     *   BoxCharacters    = naam van de struct
     *   (parameterlijst) = parameters voor de constructor. omdat het een record is, worden er automatisch propeties aangemaak op basis van de lijst die hier wordt opgegeven.
     */
}
