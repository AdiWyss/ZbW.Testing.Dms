namespace ZbW.Testing.Dms.Client.Repositories
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class ComboBoxItems
    {
        public static List<string> Typ =>
            new List<string>
                {
                    "Verträge",
                    "Quittungen"
                };
    }
}