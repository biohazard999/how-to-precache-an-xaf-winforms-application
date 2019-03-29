using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using DevExpress.ExpressApp.Win.Utils;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Svg;
using DevExpress.XtraSplashScreen;

namespace PreCacheDemo.Win {
    public partial class XafSplashScreen : SplashScreen {
        protected override void DrawContent(GraphicsCache graphicsCache, Skin skin) {
            Rectangle bounds = ClientRectangle;
            bounds.Width--; bounds.Height--;
            graphicsCache.Graphics.DrawRectangle(graphicsCache.GetPen(Color.FromArgb(255, 87, 87, 87), 1), bounds);
        }
        protected void UpdateLabelsPosition() {
            applicationNameLabel.CalcBestSize();
            int newLeft = (Width - applicationNameLabel.Width) / 2;
            applicationNameLabel.Location = new Point(newLeft, applicationNameLabel.Top);
            subtitleLabel.CalcBestSize();
            newLeft = (Width - subtitleLabel.Width) / 2;
            subtitleLabel.Location = new Point(newLeft, subtitleLabel.Top);
        }
        public XafSplashScreen() {
            InitializeComponent();
            this.labelControl1.Text = "Copyright © " + DateTime.Now.Year.ToString() + " Company Name" +  System.Environment.NewLine + "All right reserved.";

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            this.pictureEdit1.SvgImage = SvgImage.FromResources(executingAssembly.GetName().Name + ".Images.Logotype.svg", executingAssembly);

            UpdateLabelsPosition();
        }
        
        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg) {
            base.ProcessCommand(cmd, arg);
            if((UpdateSplashCommand)cmd == UpdateSplashCommand.Description) {
                labelControl2.Text = (string)arg;
            }
        }
        
        #endregion

        public enum SplashScreenCommand {
        }
    }
}