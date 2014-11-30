using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace VSBookmarks {

    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid("406FEC6A-89A5-44EA-BF41-70A61ADF90B6")]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    class VSBookmarksPackage : Package {
    }

}