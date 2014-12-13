using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace VSBookmarks {
  [Export(typeof(IKeyProcessorProvider))]
  [TextViewRole(PredefinedTextViewRoles.Document)]
  [ContentType("text")]
  [Name("KeyProcessorProvider")]
  [Order(Before = "VisualStudioKeyProcessor")]
  class KeyProcessorProvider: IKeyProcessorProvider {

    [ImportingConstructor]
    public KeyProcessorProvider() {
    }

    public KeyProcessor GetAssociatedProcessor(IWpfTextView wpfTextView) {
      return new BookmarkKeyProcessor(wpfTextView);
    }
  }

}