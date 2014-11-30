using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.OLE.Interop;
using System.Runtime.InteropServices;

namespace VSBookmarks {

  internal class KeyFilter: IOleCommandTarget {

    public KeyFilter(ITextView textView, Manager manager) {
      _TextView = textView;
      _Manager = manager;
    }

    public void AddKeyFilter(IVsEditorAdaptersFactoryService editorFactory) {
      // some legacy shit
      var vsTextView = editorFactory.GetViewAdapter(_TextView);
      var wpfTextView = editorFactory.GetWpfTextView(vsTextView);

      IOleCommandTarget next;
      int hr = vsTextView.AddCommandFilter(this, out next);

      if(hr == VSConstants.S_OK) {
        if(next != null)
          _NextTarget = next;
      }
    }

    int IOleCommandTarget.QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText) {
      if(pguidCmdGroup == Guid.Parse("34CE31E6-F674-46D1-94CD-E24963677290") && prgCmds != null) {
        for(int i = 0; i < prgCmds.Length; i++)
          prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_SUPPORTED);
        return VSConstants.S_OK;
      }
      return _NextTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
    }

    int IOleCommandTarget.Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut) {
      if(pguidCmdGroup == Guid.Parse("34CE31E6-F674-46D1-94CD-E24963677290")) {
        if((nCmdID >= 0) && (nCmdID <= 9))
          _Manager.SetBookmark(Convert.ToInt32(nCmdID));
        else
          _Manager.GotoBookmark(Convert.ToInt32(nCmdID - 10));
        return VSConstants.S_OK;
      }
      return _NextTarget.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
    }

    private ITextView _TextView;
    private Manager _Manager;
    internal IOleCommandTarget _NextTarget;

  }

}