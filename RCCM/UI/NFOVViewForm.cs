using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM.UI
{
    /// <summary>
    /// Form for displaying NFOV live image and measurement overlay
    /// </summary>
    public partial class NFOVViewForm : Form
    {
        /// <summary>
        /// The RCCM system object. Used for calculating NFOV position
        /// </summary>
        protected readonly RCCMSystem rccm;
        /// <summary>
        /// List of cracks that are being measured
        /// </summary>
        protected readonly ObservableCollection<MeasurementSequence> cracks;
        /// <summary>
        /// NFOV camera that is displayed by this window
        /// </summary>
        protected readonly NFOV camera;
        /// <summary>
        /// Parent stage of this camera
        /// </summary>
        protected RCCMStage stage;
        /// <summary>
        /// Currently selected crack index. The selected crack will be edited by other controls
        /// </summary>
        protected int ActiveIndex { get; set; }
        /// <summary>
        /// Currently selected point in ListPoints. This point is indicated in the image display
        /// </summary>
        protected int ActivePoint { get; set; }
        /// <summary>
        /// Ratio of picture box size to actual NFOV image dimensions
        /// </summary>
        protected float displayScale
        {
            get { return (float)(this.nfovImage.Width) / (float)(this.camera.PixelWidth); }
        }
        /// <summary>
        /// Indicates whether or not user is drawing a line with mouse
        /// </summary>
        protected bool Drawing { get; private set; }
        /// <summary>
        /// Point where line user is drawing begins
        /// </summary>
        protected PointF drawnLineStart;
        /// <summary>
        /// Point where line user is drawing ends
        /// </summary>
        protected PointF drawnLineEnd;
        /// <summary>
        /// Timer for calling NFOV display repaint
        /// </summary>
        protected Timer nfovRepaintTimer;
        
        /// <summary>
        /// Initialize NFOV display
        /// </summary>
        /// <param name="rccm">RCCMSystem object, needed for getting location and zoom status</param>
        /// <param name="camera">NFOV camera to display</param>
        /// <param name="cracks">List of cracks to display</param>
        public NFOVViewForm(RCCMSystem rccm, NFOV camera, ObservableCollection<MeasurementSequence> cracks)
        {
            this.rccm = rccm;
            this.camera = camera;
            this.stage = camera == rccm.NFOV1 ? RCCMStage.RCCM1 : RCCMStage.RCCM2;
            this.cracks = cracks;
            this.cracks.CollectionChanged += cracksChangedHandler;
            this.Drawing = false;
            this.ActiveIndex = -1;
            this.ActivePoint = -1;
            this.nfovRepaintTimer = new Timer();
            this.nfovRepaintTimer.Interval = (int)Program.Settings.json["repaint period"];
            InitializeComponent();
            this.updateMeasurementControls();
        }

        /// <summary>
        /// Start all user controls that require UI initialization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NFOVViewForm_Load(object sender, EventArgs e)
        {
            this.Text = this.stage == RCCMStage.RCCM1 ? "NFOV 1" : "NFOV 2";

            this.camera.Initialize();

            this.nfovRepaintTimer.Tick += new EventHandler(refreshNfov);
            this.nfovRepaintTimer.Start();

            if (!this.camera.Connected)
            {
                this.disableNfovControls();
            }
            else
            {
                this.btnNfovStart.Enabled = false;
                this.btnNfovStop.Enabled = true;
            }
        }

        /// <summary>
        /// Stop camera and overlay update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NFOVViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.nfovRepaintTimer.Stop();
            string camName = this.stage == RCCMStage.RCCM1 ? "nfov 1" : "nfov 2";
            Program.Settings.json[camName]["height"] = this.camera.PixelHeight;
            Program.Settings.json[camName]["width"] = this.camera.PixelWidth;
            this.camera.Disconnect();
        }

        /// <summary>
        /// Create segment that was being drawn by user with mouse input. This will add a point or segment to the active MeasurementSequence
        /// </summary>
        public void createSegment()
        {
            PointF pos = this.rccm.GetNFOVLocation(this.stage, CoordinateSystem.Global);
            // Add measurements for start and end if user is drawing both
            if (this.cracks[this.ActiveIndex].CountPoints == 0)
            {
                // Convert pixel coordinates to global coordinates
                Measurement p0 = new Measurement(this.cracks[this.ActiveIndex], this.rccm, this.drawnLineStart.X - pos.X, this.drawnLineStart.Y - pos.Y);
                this.cracks[ActiveIndex].AddPoint(p0);
            }
            // Add end point (in all situations)
            Measurement p1 = new Measurement(this.cracks[this.ActiveIndex], this.rccm, this.drawnLineEnd.X - pos.X,
                                                                                       this.drawnLineEnd.Y - pos.Y);
            this.cracks[ActiveIndex].AddPoint(p1);
            this.Drawing = false;
            
            if (!this.camera.CheckFOV(this.rccm))
            {
                MessageBox.Show("Warning: Calibration FOV does not match current conditions");
            }
        }

        /// <summary>
        /// Move the end point of the line segment that user is currently drawing
        /// </summary>
        /// <param name="x">Mouse x location in pixels</param>
        /// <param name="y">Mouse y location in pixels</param>
        /// <param name="w">Canvas width in pixels</param>
        /// <param name="h">Canvas height in pixels</param>
        public void moveDrawnLineEnd(int x, int y, int w, int h)
        {
            if (this.Drawing)
            {
                // Get position of NFOV camera
                PointF pos = this.rccm.GetNFOVLocation(this.stage, CoordinateSystem.Global);
                // Convert mouse coordinates to global coordinate system units
                double pixX = this.camera.Scale / 1000.0 * (x - w / 2.0) / this.displayScale;
                double pixY = this.camera.Scale / 1000.0 * (y - h / 2.0) / this.displayScale;
                // Rotate pixel vector into global position vector
                PointF pix = this.rccm.FineVectorToGlobalVector(pixX, pixY);
                this.drawnLineEnd.X = pos.X + pix.X;
                this.drawnLineEnd.Y = pos.Y + pix.Y;
            }
        }

        /// <summary>
        /// Begin drawing new segment in active MeasurementSequence from user mouse input
        /// </summary>
        /// <param name="x">Mouse x location in pixels</param>
        /// <param name="y">Mouse y location in pixels</param>
        /// <param name="w">Canvas width in pixels</param>
        /// <param name="h">Canvas height in pixels</param>
        public void createDrawnLine(int x, int y, int w, int h)
        {
            if (this.crackIndexValid())
            {
                this.Drawing = true;
                // Get mouse location in global coordinates
                PointF pos = this.rccm.GetNFOVLocation(this.stage, CoordinateSystem.Global);
                double pixX = this.camera.Scale / 1000.0 * (x - w / 2.0) / this.displayScale;
                double pixY = this.camera.Scale / 1000.0 * (y - h / 2.0) / this.displayScale;
                PointF pix = this.rccm.FineVectorToGlobalVector(pixX, pixY);
                // Create start point - use last crack vertex if active crack is started.
                if (this.cracks[this.ActiveIndex].CountPoints > 0)
                {
                    Measurement p0 = this.cracks[this.ActiveIndex].GetLastPoint();
                    this.drawnLineStart.X = (float)p0.X;
                    this.drawnLineStart.Y = (float)p0.Y;
                }
                else
                {
                    this.drawnLineStart.X = pos.X + pix.X;
                    this.drawnLineStart.Y = pos.Y + pix.Y;
                }
                // Create end point
                this.drawnLineEnd.X = pos.X + pix.X;
                this.drawnLineEnd.Y = pos.Y + pix.Y;
            }
        }

        /// <summary>
        /// Create new point in active measurement sequence at mouse location
        /// </summary>
        /// <param name="x">Mouse x location in pixels</param>
        /// <param name="y">Mouse y location in pixels</param>
        /// <param name="w">Canvas width in pixels</param>
        /// <param name="h">Canvas height in pixels</param>
        public void createPoint(int x, int y, int w, int h)
        {
            if (this.crackIndexValid())
            {
                // Get mouse location in global coordinates
                PointF pos = this.rccm.GetNFOVLocation(this.stage, CoordinateSystem.Global);
                double pixX = this.camera.Scale / 1000.0 * (x - w / 2.0) / this.displayScale;
                double pixY = this.camera.Scale / 1000.0 * (y - h / 2.0) / this.displayScale;
                PointF pix = this.rccm.FineVectorToGlobalVector(pixX, pixY);
                Measurement p0 = new Measurement(this.cracks[this.ActiveIndex], this.rccm, pix.X, pix.Y);
                this.cracks[this.ActiveIndex].AddPoint(p0);
                
                if (!this.camera.CheckFOV(this.rccm))
                {
                    MessageBox.Show("Warning: Calibration FOV does not match current conditions");
                }
            }
        }

        #region UI Callbacks

        /// <summary>
        /// Start mouse measurement. Left click places a point, right a line
        /// </summary>
        private void nfovImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.createPoint(e.X, e.Y, this.nfovImage.Width, this.nfovImage.Height);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.createDrawnLine(e.X, e.Y, this.nfovImage.Width, this.nfovImage.Height);
            }
        }

        /// <summary>
        /// If right mouse pressed, move point user is drawing
        /// </summary>
        private void nfovImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Drawing)
            {
                // Move end of line to mouse location
                this.moveDrawnLineEnd(e.X, e.Y, this.nfovImage.Width, this.nfovImage.Height);
            }
        }

        /// <summary>
        /// Create segment user was drawing with right mouse button
        /// </summary>
        private void nfovImage_MouseUp(object sender, MouseEventArgs e)
        {
            int index = this.listMeasurements.SelectedIndex;
            if (this.Drawing)
            {
                this.createSegment();
            }
            // Refresh list of points
            this.updateMeasurementControls();
        }

        /// <summary>
        /// Draw NFOV image and cracks on given graphics object
        /// </summary>
        /// <param name="g">Canvas on which NFOV display will be drawn</param>
        private void nfovImage_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ResetTransform(); // Reset transform to draw NFOV image
            Pen pen = new Pen(Color.FromArgb(128, Color.Black), 0);
            // Display live image from NFOV camera
            if (this.camera.Connected && this.camera.ProcessedImage.dataSize != 0)
            {
                Bitmap img = new Bitmap(this.camera.GetLiveImage(), 612, 512);
                e.Graphics.DrawImage(img, 0, 0, 612, 512);
                img.Dispose();
            }
            // Draw crosshair
            if (this.checkCrosshair.Checked)
            {
                float midX = e.Graphics.ClipBounds.Width / 2.0f;
                float midY = e.Graphics.ClipBounds.Height / 2.0f;
                // Draw crosshair
                e.Graphics.DrawLine(pen, 0, midY, e.Graphics.ClipBounds.Width, midY);
                e.Graphics.DrawLine(pen, midX, 0, midX, e.Graphics.ClipBounds.Height);
            }

            // Now transform to world coordinates

            // Move image center to origin and rotate
            RectangleF bounds = e.Graphics.VisibleClipBounds;
            e.Graphics.TranslateTransform(bounds.Width / 2, bounds.Height / 2);
            e.Graphics.RotateTransform((float)rccm.FineStageAngle);
            e.Graphics.TranslateTransform(-bounds.Width / 2, -bounds.Height / 2);
            // Scale coordinate system by pixel to mm scaling
            float scaleX = bounds.Width / (float)this.camera.Width;
            float scaleY = bounds.Height / (float)this.camera.Height;
            e.Graphics.ScaleTransform(scaleX, scaleY);
            // Draw ruler (ticks on crosshair at defined spacing)
            if (this.checkCrosshair.Checked)
            {
                float size = (float)Program.Settings.json["ruler spacing"] / 1000.0f;
                float x, y;
                float midX = e.Graphics.ClipBounds.X + e.Graphics.ClipBounds.Width / 2;
                float midY = e.Graphics.ClipBounds.Y + e.Graphics.ClipBounds.Height / 2;
                x = midX + size;
                while (x < e.Graphics.ClipBounds.X + e.Graphics.ClipBounds.Width)
                {
                    float tempSize = Math.Round(Math.Abs(x - midX) / size) == 10.0 ? size : size / 2f;
                    e.Graphics.DrawLine(pen, x, midY - tempSize, x, midY + tempSize);
                    x += size;
                }
                x = midX - size;
                while (x > e.Graphics.ClipBounds.X)
                {
                    float tempSize = Math.Round(Math.Abs(x - midX) / size) == 10.0 ? size : size / 2f;
                    e.Graphics.DrawLine(pen, x, midY - tempSize, x, midY + tempSize);
                    x -= size;
                }
                y = midY + size;
                while (y < e.Graphics.ClipBounds.Y + e.Graphics.ClipBounds.Height)
                {
                    float tempSize = Math.Round(Math.Abs(y - midY) / size) == 10.0 ? size : size / 2f;
                    e.Graphics.DrawLine(pen, midX - tempSize, y, midX + tempSize, y);
                    y += size;
                }
                y = midY - size;
                while (y > e.Graphics.ClipBounds.Y)
                {
                    float tempSize = Math.Round(Math.Abs(y - midY) / size) == 10.0 ? size : size / 2f;
                    e.Graphics.DrawLine(pen, midX - tempSize, y, midX + tempSize, y);
                    y -= size;
                }
            }            
            // Move to NFOV location (first move origin to image center)
            e.Graphics.TranslateTransform((float)this.camera.Width / 2, (float)this.camera.Height / 2);
            PointF pos = this.rccm.GetNFOVLocation(this.stage, CoordinateSystem.Global);
            e.Graphics.TranslateTransform(-pos.X, -pos.Y);

            // Draw each crack on the image
            foreach (MeasurementSequence crack in cracks)
            {
                crack.Plot(e.Graphics, scaleX);
            }
            // Draw segment that user is creating with mouse
            if (this.crackIndexValid() && this.Drawing)
            {
                Color c = cracks[ActiveIndex].Color;
                e.Graphics.DrawLine(new Pen(Color.FromArgb(128, c), cracks[ActiveIndex].LineSize / scaleX), this.drawnLineStart, this.drawnLineEnd);
            }
            // Highlight selected point
            if (this.pointIndexValid())
            {
                MeasurementSequence crack = this.cracks[this.ActiveIndex];
                Measurement m = crack.GetPoint(this.ActivePoint);
                RectangleF point = new RectangleF(0, 0, 10.0f * crack.LineSize / scaleX, 10.0f * crack.LineSize / scaleX);
                point.X = (float)m.X - point.Width / 2.0f;
                point.Y = (float)m.Y - point.Height / 2.0f;
                e.Graphics.FillEllipse(new SolidBrush(crack.Color), point);
            }
        }

        /// <summary>
        /// Start NFOV image capture
        /// </summary>
        private void btnNfovStart_Click(object sender, EventArgs e)
        {
            this.camera.Start();
            this.nfovRepaintTimer.Start();

            btnNfovStart.Enabled = false;
            btnNfovStop.Enabled = true;
            btnNfovSnap.Enabled = true;
            btnNFOVSave.Enabled = true;
            btnNfovRecord.Enabled = true;
        }


        /// <summary>
        /// Stop NFOV image capture
        /// </summary>
        private void btnNfovStop_Click(object sender, EventArgs e)
        {
            this.camera.Stop();
            this.nfovRepaintTimer.Stop();

            btnNfovStart.Enabled = true;
            btnNfovStop.Enabled = false;
            btnNfovSnap.Enabled = false;
            btnNFOVSave.Enabled = false;
            btnNfovRecord.Enabled = false;
        }
        
        /// <summary>
        /// Save NFOV live image to a specific file
        /// </summary>
        private void btnNFOVSave_Click(object sender, EventArgs e)
        {
            Bitmap img;
            try
            {
                img = this.camera.GetLiveImage();
            }
            catch (Exception ex)
            {
                Logger.Out(ex.Message);
                return;
            }

            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            string camName = this.stage == RCCMStage.RCCM1 ? "nfov 1" : "nfov 2";
            string dir = (string)Program.Settings.json[camName]["image directory"];
            this.saveFileDialog.Title = "Select image save location";
            this.saveFileDialog.FileName = camName + timestamp + ".bmp";
            DialogResult result = this.saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                img.Save(this.saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Repaint live image
        /// </summary>
        private void refreshNfov(object sender, EventArgs e)
        {
            this.nfovImage.Invalidate();
        }

        /// <summary>
        /// Open property dialog for NFOV camera
        /// </summary>
        private void btnNfovProperties_Click(object sender, EventArgs e)
        {
            this.camera.ShowPropertiesDlg();
        }

        /// <summary>
        /// Capture image with current timestamp as name
        /// </summary>
        private void btnNfovSnap_Click(object sender, EventArgs e)
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            string camName = this.stage == RCCMStage.RCCM1 ? "nfov 1" : "nfov 2";
            string dir = (string)Program.Settings.json[camName]["image directory"];
            Logger.Out(dir + @"\" + timestamp + ".bmp");
            this.camera.Snap(dir + @"\" + timestamp + ".bmp");
        }

        /// <summary>
        /// Begin/stop recording video to file named with timestamp
        /// </summary>
        private void btnNfovRecord_Click(object sender, EventArgs e)
        {
            if (this.camera.Recording)
            {
                // Stop recording
                this.camera.Recording = false;
                btnNfovRecord.BackColor = Color.Transparent;
                MessageBox.Show("Recording stopped");
                btnNfovStart.Enabled = true;
                btnNfovStop.Enabled = true;
                btnNfovSnap.Enabled = true;
            }
            else
            {
                // Start recording
                string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
                string camName = this.stage == RCCMStage.RCCM1 ? "nfov 1" : "nfov 2";
                string dir = (string)Program.Settings.json[camName]["video directory"];
                this.camera.Record(dir + "\\" + timestamp + ".avi");
                // Color button gray to indicate that it is pressed
                btnNfovRecord.BackColor = Color.Gray;
                btnNfovStart.Enabled = false;
                btnNfovStop.Enabled = false;
                btnNfovSnap.Enabled = false;
            }
        }

        /// <summary>
        /// Change selected crack and clear point selection
        /// </summary>
        private void listMeasurements_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveIndex = this.listMeasurements.SelectedIndex;
            this.ActivePoint = -1;
        }

        /// <summary>
        /// Open GUI for editting selected crack
        /// </summary>
        private void btnEditSequence_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                MeasurementSequence crack = this.cracks[this.ActiveIndex];
                NewMeasurementForm form = new NewMeasurementForm(crack);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Check if name is unique
                    MeasurementSequence crack2 = findCrackName(form.GetName());
                    if (crack2 == null || crack2 == crack)
                    {
                        crack.Name = form.GetName();
                        crack.Color = Color.FromArgb(128, form.GetColor());
                        crack.LineSize = form.GetLineSize();
                        crack.Orientation = form.GetOrientation();
                        crack.Mode = form.GetMode();
                        crack.Camera = form.GetCamera();
                        this.updateMeasurementControls();
                    }
                    else
                    {
                        MessageBox.Show("Please use a unique name");
                    }
                }
            }
        }

        /// <summary>
        /// Create new measurement
        /// </summary>
        private void btnNewSequence_Click(object sender, EventArgs e)
        {
            string cameraName = this.stage == RCCMStage.RCCM1 ? "nfov 1" : "nfov 2";
            NewMeasurementForm dlg = new NewMeasurementForm("Crack " + MeasurementSequence.CrackCount, cameraName);
            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                MeasurementSequence newCrack = new MeasurementSequence(dlg);
                // Check that name is unique
                MeasurementSequence crack2 = findCrackName(newCrack.Name);
                if (crack2 == null)
                {
                    this.cracks.Add(newCrack);
                    this.listMeasurements.SelectedIndex = this.cracks.Count - 1;
                }
                else
                {
                    MessageBox.Show("Please use a unique name");
                }
            }
            dlg.Dispose();
        }

        /// <summary>
        /// Deleted selected crack
        /// </summary>
        private void btnDeleteSequence_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                this.cracks.RemoveAt(this.ActiveIndex);
                this.listMeasurements.SelectedIndex = -1;
            }
        }
        
        /// <summary>
        /// Save selected crack to user selected file
        /// </summary>
        private void btnSaveCrack_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                this.saveFileDialog.FileName = this.cracks[this.ActiveIndex].GetFileName();
                DialogResult result = this.saveFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.cracks[this.ActiveIndex].WriteToFile(this.saveFileDialog.FileName, false);
                }
            }
        }

        /// <summary>
        /// Add a measurement at center of image
        /// </summary>
        private void btnCrosshairMeasure_Click(object sender, EventArgs e)
        {
            if (this.crackIndexValid())
            {
                Measurement pt = new Measurement(this.cracks[this.ActiveIndex], this.rccm, 0, 0);
                this.cracks[this.ActiveIndex].AddPoint(pt);
                Logger.Out(this.cracks[this.ActiveIndex].ToString());
                // Refresh list of points
                this.updateMeasurementControls();
            }
        }

        /// <summary>
        /// Move actuators to their position when selected measurement was captured
        /// </summary>
        private void btnGotoPoint_Click(object sender, EventArgs e)
        {
            if (this.pointIndexValid())
            {
                Measurement m = this.cracks[this.ActiveIndex].GetPoint(this.ActivePoint);
                if (this.cracks[this.ActiveIndex].Camera == "nfov 1")
                {
                    this.rccm.motors["fine 1 X"].SetPos(m.FineX);
                    this.rccm.motors["fine 1 X"].WaitForEndOfMove();
                    this.rccm.motors["fine 1 Y"].SetPos(m.FineY);
                    this.rccm.motors["fine 1 Y"].WaitForEndOfMove();
                    this.rccm.motors["fine 1 Z"].SetPos(m.FineZ);
                }
                else
                {
                    this.rccm.motors["fine 2 X"].SetPos(m.FineX);
                    this.rccm.motors["fine 2 X"].WaitForEndOfMove();
                    this.rccm.motors["fine 2 Y"].SetPos(m.FineY);
                    this.rccm.motors["fine 2 Y"].WaitForEndOfMove();
                    this.rccm.motors["fine 2 Z"].SetPos(m.FineZ);
                }
            }
        }

        /// <summary>
        /// Delete selected point from measurement sequence
        /// </summary>
        private void btnDeletePoint_Click(object sender, EventArgs e)
        {
            if (this.pointIndexValid())
            {
                this.cracks[this.ActiveIndex].removePoint(this.ActivePoint);
                this.listPoints.SelectedIndices.Clear();
                // Refresh list of points
                this.updateMeasurementControls();
            }
        }

        /// <summary>
        /// Change selected point
        /// </summary>
        private void listPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listPoints.SelectedIndices.Count > 0)
            {
                this.ActivePoint = this.listPoints.SelectedIndices[0];
            }
        }

        #endregion

        /// <summary>
        /// Enable buttons for controlling NFOV
        /// </summary>
        private void enableNfovControls()
        {
            btnNfovStart.Enabled = true;
            btnNfovStop.Enabled = true;
            btnNfovSnap.Enabled = true;
            btnNfovRecord.Enabled = true;
            btnNfovProperties.Enabled = true;
        }

        /// <summary>
        /// Disable buttons for controlling NFOV
        /// </summary>
        private void disableNfovControls()
        {
            btnNfovStart.Enabled = false;
            btnNfovStop.Enabled = false;
            btnNfovSnap.Enabled = false;
            btnNfovRecord.Enabled = false;
            btnNfovProperties.Enabled = false;
        }

        /// <summary>
        /// Refresh lists of cracks and points
        /// </summary>
        private void updateMeasurementControls()
        {
            // Update list of cracks
            int ind = this.ActiveIndex;
            this.listMeasurements.Items.Clear();
            for (int i = 0; i < this.cracks.Count; i++)
            {
                this.listMeasurements.Items.Add(this.cracks[i].Name);
            }
            if (this.crackIndexValid())
            {
                this.listMeasurements.SelectedIndex = this.ActiveIndex;
            }
            // Update list of points
            if (this.crackIndexValid())
            {
                this.listPoints.Items.Clear();
                for (int i = 0; i < this.cracks[this.ActiveIndex].CountPoints; i++)
                {
                    Measurement m = this.cracks[this.ActiveIndex].GetPoint(i);
                    this.listPoints.Items.Add(new ListViewItem(new string[] {
                        string.Format("{0}", m.Cycle),
                        string.Format("{0:0.00}", m.CrackLength)
                    }));
                }
            }
        }

        /// <summary>
        /// Ensure that selected crack index is a valid array index of cracks
        /// </summary>
        private bool crackIndexValid()
        {
            return this.ActiveIndex >= 0 && this.ActiveIndex < this.cracks.Count;
        }

        /// <summary>
        /// Ensure that selected point index is a valid array index of active crack points
        /// </summary>
        private bool pointIndexValid()
        {
            return this.crackIndexValid() && this.ActivePoint >= 0 && this.ActivePoint < this.cracks[this.ActiveIndex].CountPoints;
        }

        /// <summary>
        /// When cracks is modified, this event handler is called to refresh user displayed info
        /// </summary>
        private void cracksChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.updateMeasurementControls();
        }

        /// <summary>
        /// Check if there is already a crack by this name
        /// </summary>
        private MeasurementSequence findCrackName(string name)
        {
            foreach (MeasurementSequence crack in this.cracks)
            {
                if (crack.Name == name)
                {
                    return crack;
                }
            }
            return null;
        }
    }
}
