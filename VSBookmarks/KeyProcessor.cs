using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Input;

namespace VSBookmarks {

  class BookmarkKeyProcessor: KeyProcessor {
                            
    public BookmarkKeyProcessor(ITextView textView) {
      _Manager = textView.Properties.GetOrCreateSingletonProperty<Manager>(() => new Manager(textView.TextBuffer));
    }

    public override void KeyDown(KeyEventArgs args) {
      if((Keyboard.Modifiers & ModifierKeys.Control) == 0)
        return;
      // When pressing AltGr in german layout Windows automatically adds Ctrl
      // (That's the way Windows distinguishes Alt and AltGr buttons)
      if((Keyboard.Modifiers & ModifierKeys.Alt) != 0)
        return;

      if(  args.Key == Key.D1
        || args.Key == Key.D2
        || args.Key == Key.D3
        || args.Key == Key.D4
        || args.Key == Key.D5
        || args.Key == Key.D6
        || args.Key == Key.D7
        || args.Key == Key.D8
        || args.Key == Key.D9
        || args.Key == Key.NumPad1
        || args.Key == Key.NumPad2
        || args.Key == Key.NumPad3
        || args.Key == Key.NumPad4
        || args.Key == Key.NumPad5
        || args.Key == Key.NumPad6
        || args.Key == Key.NumPad7
        || args.Key == Key.NumPad8
        || args.Key == Key.NumPad9                  
        ) 
      {
        bool shiftPressed = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;

        switch(args.Key) {
          case Key.D1:
          case Key.NumPad1:
            if(shiftPressed)
              _Manager.SetBookmark(1);
            else
              _Manager.GotoBookmark(1);
              break;
          case Key.D2:
          case Key.NumPad2:
              if(shiftPressed)
                _Manager.SetBookmark(2);
              else
                _Manager.GotoBookmark(2);
              break;
          case Key.D3:
          case Key.NumPad3:
              if(shiftPressed)
                _Manager.SetBookmark(3);
              else
                _Manager.GotoBookmark(3);
              break;
          case Key.D4:
          case Key.NumPad4:
              if(shiftPressed)
                _Manager.SetBookmark(4);
              else
                _Manager.GotoBookmark(4);
              break;
          case Key.D5:
          case Key.NumPad5:
              if(shiftPressed)
                _Manager.SetBookmark(5);
              else
                _Manager.GotoBookmark(5);
              break;
          case Key.D6:
          case Key.NumPad6:
              if(shiftPressed)
                _Manager.SetBookmark(6);
              else
                _Manager.GotoBookmark(6);
              break;
          case Key.D7:
          case Key.NumPad7:
              if(shiftPressed)
                _Manager.SetBookmark(7);
              else
                _Manager.GotoBookmark(7);
              break;
          case Key.D8:
          case Key.NumPad8:
              if(shiftPressed)
                _Manager.SetBookmark(8);
              else
                _Manager.GotoBookmark(8);
              break;
          case Key.D9:
          case Key.NumPad9:
              if(shiftPressed)
                _Manager.SetBookmark(9);
              else
                _Manager.GotoBookmark(9);
              break;        
          default:
            return;
        }
        args.Handled = true;
      }
    }

    private Manager _Manager;
  }
}