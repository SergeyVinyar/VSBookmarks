using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace VSBookmarks {

  [Export(typeof(IGlyphFactoryProvider))]
  [Name("VSBookmarks")]
  [Order(After = "VsTextMarker")]
  [ContentType("code")]
  [TagType(typeof(Tag))]
  class GlyphFactoryProvider: IGlyphFactoryProvider {

    public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin) {
      return new GlyphFactory();
    }

  }

}