namespace RpgManagerConsole
{
    /// <summary>
    /// Used by the <see cref="ConsoleDraw"/> class to represent a set of characters used to draw boxes in the console.
    /// </summary>
    internal readonly record struct BoxCharacters(char Horizontal, char Vertical, char UpperLeft, char UpperRight, char LowerLeft, char LowerRight);

    // Gebruik van records is buiten de scope van de cursus, maar wordt hier gebruikt om snel een alleen-lezen struct te definiëren met een constructor en properties.
    /*
     * Van links naar rechts:
     *   internal         = access modifier (zie slides)
     *   readonly         = maakt de struct immutable (properties kunnen niet aangepast worden na initialisatie)
     *   record           = compiler schrijft automatisch constructor, properties, Equals, GetHashCode en ToString op basis van de parameterlijst
     *   struct           = value type (in tegenstelling tot een class, wat een reference type is) voor performantie
     *   BoxCharacters    = naam van de struct
     *   (parameterlijst) = parameters voor de constructor. omdat het een record is, worden er automatisch propeties aangemaak op basis van de lijst die hier wordt opgegeven.
     */
}
