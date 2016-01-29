using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using EnvDTE;
using EnvDTE80;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using System.ComponentModel.Design;

namespace VSBookmarks {

  internal class Tag: IGlyphTag {
    public readonly int Number;

    public Tag(int number) {
      Number = number;
    }
  }

  internal class Bookmark {
    public readonly TrackingTagSpan<Tag> Span;

    public Bookmark(TrackingTagSpan<Tag> span) {
      Span = span;
    }

    public int GetRow(ITextBuffer buffer) {
      return Span.Span
        .GetStartPoint(buffer.CurrentSnapshot)
        .GetContainingLine()
        .LineNumber + 1; // LineNumber is zero-based
    }
  }

  internal class Manager {

    public Manager() {
      _DTE2 = Package.GetGlobalService(typeof(DTE)) as DTE2;
    }

    public SimpleTagger<Tag> Tagger {
      get {
        if(_Buffer == null)
          throw new Exception("_Buffer is not initialized");
        if(_Tagger == null)
          _Tagger = new SimpleTagger<Tag>(_Buffer);
        return _Tagger;
      } 
    }
    private SimpleTagger<Tag> _Tagger = null;

    public void SetBuffer(ITextBuffer buffer) {
      if(buffer == null)
        throw new ArgumentNullException("buffer");
      _Buffer = buffer;
      _Tagger = null;
    }

    public void SetBookmark(int number) {
      TextSelection selection = _DTE2.ActiveDocument.Selection;
      int row = selection.ActivePoint.Line;

      var bookmark = _Bookmarks[number];
      if(bookmark != null) {
        // Remove old bookmark
        var oldRow = bookmark.GetRow(_Buffer);
        Tagger.RemoveTagSpan(bookmark.Span);
        _Bookmarks[number] = null;
        if(oldRow == row)
          return;
      }

      // Add new bookmark
      var snapshot = _Buffer.CurrentSnapshot;
      var line = snapshot.GetLineFromLineNumber(row - 1);
      var span = snapshot.CreateTrackingSpan(new SnapshotSpan(line.Start, 0), SpanTrackingMode.EdgeExclusive);
      var tagTrackerSpan = Tagger.CreateTagSpan(span, new Tag(number));
      _Bookmarks[number] = new Bookmark(tagTrackerSpan);
    }

    public Bookmark GotoBookmark(int number) {
      var bookmark = _Bookmarks[number];
      if(bookmark == null)
        return null;

      var row = _Bookmarks[number].GetRow(_Buffer);
      TextSelection selection = _DTE2.ActiveDocument.Selection;
      int column = selection.ActivePoint.DisplayColumn;
      selection.StartOfDocument();
      selection.MoveToLineAndOffset(row, column);
                                            
      return _Bookmarks[number];
    }

    public void RemoveBookmark(int number) {
      var bookmark = _Bookmarks[number];
      if(bookmark == null)
        return;
      Tagger.RemoveTagSpan(bookmark.Span);
      _Bookmarks[number] = null;
    }

    private ITextBuffer _Buffer = null;
    private Bookmark[] _Bookmarks = new Bookmark[10];

    private DTE2 _DTE2;
  }
}
