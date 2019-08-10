namespace EnumGenerator.Editor
{
    /// <summary>
    /// Type of output to produce.
    /// </summary>
    public enum OutputType
    {
        /// <summary>
        /// Produce a CSharp (.cs) source file.
        /// </summary>
        CSharp = 0,

        /// <summary>
        /// Produce a Cil (.il) source file.
        /// </summary>
        Cil = 1,

        /// <summary>
        /// Produce a class-library (.dll) file.
        /// </summary>
        ClassLibrary = 2,

        /// <summary>
        /// Produce a FSharp (.fs) source file.
        /// </summary>
        FSharp = 3,

        /// <summary>
        /// Produce a VisualBasic (.vb) source file.
        /// </summary>
        VisualBasic = 4,
    }
}
