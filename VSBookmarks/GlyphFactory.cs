using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.PlatformUI;

namespace VSBookmarks {

  class GlyphFactory: IGlyphFactory {

    public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag) {
      if(tag == null || !(tag is Tag)) {
        return null;
      }

      var digit = new TextBlock();
      digit.Text = (tag as Tag).Number.ToString();
      digit.FontFamily = new FontFamily("Verdana");
      digit.FontSize = 12;
      digit.FontWeight = FontWeights.ExtraBold;
      digit.HorizontalAlignment = HorizontalAlignment.Center;
      digit.VerticalAlignment = VerticalAlignment.Center;
      digit.Width = _GlyphSize;
      digit.Height = _GlyphSize;
      digit.Foreground = new SolidColorBrush(GetCurrentThemeColor());

      VSColorTheme.ThemeChanged += (e) => {
        digit.Foreground = new SolidColorBrush(GetCurrentThemeColor());
      };

      return digit;
    }

    private Color GetCurrentThemeColor() {
      var drawingColor = VSColorTheme.GetThemedColor(EnvironmentColors.CommandBarTextActiveColorKey);
      return Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
    }

    const double _GlyphSize = 14.0;
  }

}