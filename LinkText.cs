// カスタムコントロールの練習
// TextBoxを継承した編集可能なLinkLabel

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpASAP
{
    public partial class LinkText: TextBox
    {
        public LinkText()
        {
            InitializeComponent();

            // property default values
            Cursor = Cursors.Hand;
            BorderStyle = BorderStyle.None;
            ForeColor = Color.FromArgb(0, 0, 255);
            if (Parent != null) 
                BackColor = Parent.BackColor;
            ActiveLinkColor = Color.FromArgb(255, 0, 0);
            LinkColor = Color.FromArgb(0, 0, 255);
            VisitedLinkColor = Color.FromArgb(128, 0, 128);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                //SelectionStart = Text.Length;
                LinkVisited = false;
            }
        }

        /* AutoSize */
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
                ApplyAutoSize();
            }
        }

        void ApplyAutoSize()
        {
            if (base.AutoSize && Text.Length != 0)
                using (var g = CreateGraphics())
                    Width = (int)g.MeasureString(Text, Font).Width;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            LinkVisited = false;
            base.OnTextChanged(e);
            ApplyAutoSize();
        }
            
        protected override void OnFontChanged(EventArgs e)
        {
            RefreshUnderline();
            base.OnFontChanged(e);
            ApplyAutoSize();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            RefreshUnderline();
            base.OnEnabledChanged(e);
            //if (!Enabled)
            //    BackColor = Color.FromArgb(255, BackColor); //??
        }

        void RefreshUnderline()
        {
            Font = new Font(Font, Enabled ? FontStyle.Underline : FontStyle.Regular);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            if (Parent != null)
                BackColor = Parent.BackColor;
            base.OnParentBackColorChanged(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
                BackColor = Parent.BackColor;
            base.OnParentChanged(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (!Control.MouseButtons.HasFlag(MouseButtons.Left))
                Cursor = Cursors.IBeam;
            
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Cursor = Cursors.Hand;
            base.OnLostFocus(e);
        }

        /* mouse event handling */
        int mouse_down_x, mouse_down_y;
        int click_count = 0;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                mouse_down_x = e.X;
                mouse_down_y = e.Y;
                click_count = 1;
                ForeColor = ActiveLinkColor;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var d = SystemInformation.DragSize;
            if (click_count > 0
             && (Math.Abs(mouse_down_x - e.X) > d.Width/*4*/
              || Math.Abs(mouse_down_y - e.Y) > d.Height/*4*/)) {
                click_count = 0;
                RefreshForeColor();
                Cursor = Cursors.IBeam;
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            RefreshForeColor();
            base.OnMouseUp(e);

            //SystemInformation.DoubleClickSize
            //SystemInformation.DoubleClickTime
            //
            if (click_count > 0) {
                click_count = 0;

                if (browser_enabled)
                    OpenBrowser();

                base.OnClick(e);
                base.OnMouseClick(e);
            }
        }

        /* ForeColor switch */
        public Color ActiveLinkColor { get; set; }
        public Color LinkColor { get; set; }
        public Color VisitedLinkColor { get; set; }

        void RefreshForeColor()
        {
            ForeColor = LinkVisited ? VisitedLinkColor : LinkColor;
        }

        public bool LinkVisited
        {
            get { return _LinkVisited; }
            set
            {
                _LinkVisited = value;
                RefreshForeColor();
            }
        }

        bool _LinkVisited = false;

        /* click cancelation */
        protected override void OnClick(EventArgs e)
        {
            //base.OnClick(e); see MouseDown event
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //base.OnMouseClick(e); see MouseDown event
        }

        /* open browser */
        bool browser_enabled = true;
        public event EventHandler BrowserOpened;

        void OpenBrowser()
        {
            System.Diagnostics.Process.Start(Text);
            LinkVisited = true;

            if (BrowserOpened != null)
                BrowserOpened(this, new EventArgs());
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (browser_enabled && e.KeyChar == '\r')
                OpenBrowser();

            base.OnKeyPress(e);
        }
    }
}
