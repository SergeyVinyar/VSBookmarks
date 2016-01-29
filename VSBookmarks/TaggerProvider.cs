using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.TextManager.Interop;

namespace VSBookmarks {

  [Export(typeof(IViewTaggerProvider))]
  [ContentType("any")]
  [TagType(typeof(Tag))]
  class TodoTaggerProvider: IViewTaggerProvider {

    public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T: ITag {
      if(textView == null)
        throw new ArgumentNullException("textView");
      if(buffer == null)
        throw new ArgumentNullException("buffer");

      var manager = textView.Properties.GetOrCreateSingletonProperty<Manager>(() => new Manager());
      manager.SetBuffer(textView.TextBuffer);

      new CommandFilter(textView, manager).AddKeyFilter(editorFactory);
      return manager.Tagger as ITagger<T>;
    }

    [Import(typeof(IVsEditorAdaptersFactoryService))]
    internal IVsEditorAdaptersFactoryService editorFactory;

  }
}