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
using TIS.Imaging;

namespace RCCM.UI
{
    /// <summary>
    /// Form for displaying WFOV live image and measurement overlay
    /// </summary>
    public partial class WFOVViewForm : Form
    {
        /// <summary>
        /// The RCCM system object. Used for calculating WFOV position
        /// </summary>
        protected readonly RCCMSystem rccm;
        /// <summary>
        /// List of cracks that are being measured
        /// </summary>
        protected readonly ObservableCollection<MeasurementSequence> cracks;
        /// <summary>
        /// WFOV camera that is displayed by this window
        /// </summary>
        protected readonly WFOV camera;
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
            get { return (float)(this.wfovContainer.Bounds.Width) / (float)(WFOV.IMG_WIDTH); }
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
        /// Create form and initialize given camera
        /// </summary>
        /// <param name="rccm">Reference to the RCCM object</param>
        /// <param name="camera">Camera to be displayed</param>
        /// <param name="cracks">Reference to the list of all measurement sequences</param>
        public WFOVViewForm(RCCMSystem rccm, WFOV camera, ObservableCollection<MeasurementSequence> cracks)
        {
            this.rccm = rccm;
            this.camera = camera;
            this.stage = this.camera == rccm.WFOV1 ? RCCMStage.RCCM1 : RCCMStage.RCCM2;
            this.cracks = cracks;
            this.cracks.CollectionChanged += cracksChangedHandler;
            this.Drawing = false;
            this.ActiveIndex = -1;
            this.ActivePoint = -1;
            InitializeComponent();
            this.updateMeasurementControls();
        }

        /// <summary>
        /// Open form and start camera and perform UI initialization
        /// </summary>
        private void WFOVViewForm_Load(object sender, EventArgs e)
        {
            this.Text = this.stage == RCCMStage.RCCM1 ? "WFOV 1" : "WFOV 2";
            
            this.wfovContainer.OverlayBitmapPosition = PathPositions.Display;
            // Enable the overlay bitmap for drawing.
            OverlayBitmap ob = this.wfovContainer.OverlayBitmapAtPath[PathPositions.Display];
            ob.Enable = true;
            // Fill the overlay bitmap with the dropout color.
            ob.DropOutColor = Color.Black;
            ob.Fill(Color.Black);
            ob.ColorMode = OverlayColorModes.Color;
            this.wfovContainer.OverlayUpdate += new EventHandler<ICImagingControl.OverlayUpdateEventArgs>(wfovOverlayPaint);

            // Initialize camera
            bool success = this.camera.Initialize(this.wfovContainer);
            if (success)
            {
                btnWfovStart.Enabled = true;

                //  Setup the range of the zoom and focus sliders.
                sliderZoom.Minimum = this.camera.ZoomMin;
                sliderZoom.Maximum = this.camera.ZoomMax;
                sliderFocus.Minimum = this.camera.FocusMin;
                sliderFocus.Maximum = this.camera.FocusMax;

                //  Set the sliders to the current zoom and focus values.
                sliderZoom.Value = this.camera.Zoom;
                textZoom.Text = this.camera.Zoom.ToString();
                sliderFocus.Value = this.camera.Focus;
                textFocus.Text = this.camera.Focus.ToString();
            }
        }

        /// <summary>
        /// Stop camera on close
        /// </summary>
        private void WFOVViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.camera.Recording)
            {
                this.camera.StopRecord();
            }
            this.camera.Stop();
        }

        /// <summary>
        /// Draw cracks overlay on WFOV display
        /// </summary>
        private void wfovOverlayPaint(object sender, ICImagingControl.OverlayUpdateEventArgs e)
        {
            Graphics g = e.overlay.GetGraphics();
            g.Clear(Color.Black);
            g.ResetTransform();
            
            // Draw crosshair
            if (this.checkCrosshair.Checked)
            {
                float midX = (g.VisibleClipBounds.Right + g.VisibleClipBounds.Left) / 2.0f;
                float midY = (g.VisibleClipBounds.Bottom + g.VisibleClipBounds.Top) / 2.0f;
                Pen pen = new Pen(Color.FromArgb(128, 1, 1, 1), 2);
                g.DrawLine(pen, new PointF(g.VisibleClipBounds.Left, midY), new PointF(g.VisibleClipBounds.Right, midY));
                g.DrawLine(pen, new PointF(midX, g.VisibleClipBounds.Top), new PointF(midX, g.VisibleClipBounds.Bottom));
            }

            // Now transform to world coordinates

            // Move image center to origin and rotate
            RectangleF bounds = g.VisibleClipBounds;
            g.TranslateTransform(bounds.Width / 2, bounds.Height / 2);
            g.RotateTransform((float)rccm.FineStageAngle);
            g.TranslateTransform(-bounds.Width / 2, -bounds.Height / 2);
            // Scale coordinate system by pixel to mm scaling
            float scaleX = bounds.Width / (float)this.camera.Width;
            float scaleY = bounds.Height / (float)this.camera.Height;
            g.ScaleTransform(scaleX, scaleY);
            // Move to WFOV location (first move origin to image center)
            g.TranslateTransform((float)this.camera.Width / 2, (float)this.camera.Height / 2);
            PointF pos = this.rccm.GetWFOVLocation(this.stage, CoordinateSystem.Global);
            g.TranslateTransform(-pos.X, -pos.Y);

            // Draw each crack on the image
            foreach (MeasurementSequence crack in cracks)
            {
                crack.Plot(g, scaleX);
            }
            // Draw segment that user is creating with mouse
            if (this.crackIndexValid() && this.Drawing)
            {
                Color c = cracks[ActiveIndex].Color;
                g.DrawLine(new Pen(Color.FromArgb(128, c), cracks[ActiveIndex].LineSize / scaleX), this.drawnLineStart, this.drawnLineEnd);
            }
            // Highlight selected point
            if (this.pointIndexValid())
            {
                MeasurementSequence crack = this.cracks[this.ActiveIndex];
                Measurement m = crack.GetPoint(this.ActivePoint);
                RectangleF point = new RectangleF(0, 0, 10.0f * crack.LineSize / scaleX, 10.0f * crack.LineSize / scaleX);
                point.X = (float)m.X - point.Width / 2.0f;
                point.Y = (float)m.Y - point.Height / 2.0f;
                g.FillEllipse(new SolidBrush(crack.Color), point);
            }
            e.overlay.ReleaseGraphics(g);
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
                double pixX = this.camera.Scale * (x - w / 2.0) / this.displayScale;
                double pixY = this.camera.Scale * (y - h / 2.0) / this.displayScale;
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
                double pixX = this.camera.Scale * (x - w / 2.0) / this.displayScale;
                double pixY = this.camera.Scale * (y - h / 2.0) / this.displayScale;
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
                double pixX = this.camera.Scale * (x - w / 2.0) / this.displayScale;
                double pixY = this.camera.Scale * (y - h / 2.0) / this.displayScale;
                PointF pix = this.rccm.FineVectorToGlobalVector(pixX, pixY);
                Measurement p0 = new Measurement(this.cracks[this.ActiveIndex], this.rccm, pix.X, pix.Y);
                this.cracks[this.ActiveIndex].AddPoint(p0);
                
                if (!this.camera.CheckFOV(this.rccm))
                {
                    MessageBox.Show("Warning: Calibration FOV does not match current conditions");
                }
            }
        }

        /// <summary>
        /// Start mouse measurement. Left click places a point, right a line
        /// </summary>
        private void wfovContainer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.createPoint(e.X, e.Y, this.wfovContainer.Width, this.wfovContainer.Height);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.createDrawnLine(e.X, e.Y, this.wfovContainer.Width, this.wfovContainer.Height);
            }
        }

        /// <summary>
        /// If right mouse pressed, move point user is drawing
        /// </summary>
        private void wfovContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Drawing)
            {
                // Move end of line to mouse location
                this.moveDrawnLineEnd(e.X, e.Y, this.wfovContainer.Width, this.wfovContainer.Height);
            }
        }

        /// <summary>
        /// Create segment user was drawing with right mouse button
        /// </summary>
        private void wfovContainer_MouseUp(object sender, MouseEventArgs e)
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
        /// Start camera and change button states
        /// </summary>
        private void btnWfovStart_Click(object sender, EventArgs e)
        {
            this.camera.Start();

            // Update button states
            if (this.wfovContainer.DeviceValid)
            {
                this.enableWfovControls();
                this.btnWfovStop.Enabled = true;
            }
        }

        /// <summary>
        /// Stop camera and change button states
        /// </summary>
        private void btnWfovStop_Click(object sender, EventArgs e)
        {
            this.camera.Stop();

            // Update button states
            if (!this.wfovContainer.LiveVideoRunning)
            {
                this.disableWfovControls();
                this.btnWfovStart.Enabled = true;
            }
        }

        /// <summary>
        /// Capture image to file named with timestamp
        /// </summary>
        private void btnWfovSnap_Click(object sender, EventArgs e)
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            string camName = this.stage == RCCMStage.RCCM1 ? "wfov 1" : "wfov 2";
            string dir = (string)Program.Settings.json[camName]["image directory"];
            this.camera.Snap(dir + "\\" + timestamp + ".png");
        }

        /// <summary>
        /// Save WFOV live image to a specific file
        /// </summary>
        private void btnWFOVSave_Click(object sender, EventArgs e)
        {
            string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
            string camName = this.stage == RCCMStage.RCCM1 ? "wfov 1" : "wfov 2";
            string dir = (string)Program.Settings.json[camName]["image directory"];
            this.saveFileDialog.Title = "Select image save location";
            this.saveFileDialog.FileName = camName + timestamp + ".bmp";
            DialogResult result = this.saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.camera.Snap(this.saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// Open camera property dialog
        /// </summary>
        private void btnWfovProperties_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.camera.EditProperties();
            }
        }

        /// <summary>
        /// Start/stop recording video to file named with timestamp
        /// </summary>
        private void btnWfovRecord_Click(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                if (this.camera.Recording == false)
                {
                    string timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt-fff}", DateTime.Now);
                    string camName = this.stage == RCCMStage.RCCM1 ? "wfov 1" : "wfov 2";
                    string dir = (string)Program.Settings.json[camName]["video directory"];
                    this.camera.Record(dir + @"\" + timestamp + ".avi");
                    btnWfovRecord.BackColor = Color.Gray;
                    btnWfovStart.Enabled = false;
                    btnWfovSnap.Enabled = false;
                }
                else
                {
                    this.camera.StopRecord();
                    MessageBox.Show("Recording stopped");
                    btnWfovRecord.BackColor = Color.Transparent;
                    btnWfovStart.Enabled = true;
                    btnWfovSnap.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Autofocus the camera
        /// </summary>
        private void btnFocus_Click(object sender, EventArgs e)
        {
            btnFocus.Enabled = false;
            int newFocus = this.camera.AutoFocus();
            this.sliderFocus.Value = newFocus;
            this.textFocus.Text = newFocus.ToString();
            btnFocus.Enabled = true;
        }

        /// <summary>
        /// Change zoom level of the camera based on slider value
        /// </summary>
        private void sliderZoom_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.camera.Zoom = sliderZoom.Value;
                textZoom.Text = sliderZoom.Value.ToString();
            }
        }

        /// <summary>
        /// Change focus level of the camera based on slider value
        /// </summary>
        private void sliderFocus_Scroll(object sender, EventArgs e)
        {
            if (wfovContainer.DeviceValid)
            {
                this.camera.Focus = sliderFocus.Value;
                textFocus.Text = sliderFocus.Value.ToString();
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
                Console.WriteLine(crack.Mode);
                NewMeasurementForm form = new NewMeasurementForm(crack);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    MeasurementSequence crack2 = findCrackName(form.GetName());
                    // Check if name is unique
                    if (crack2 == null || crack2 == crack)
                    {
                        crack.Name = form.GetName();
                        crack.Color = Color.FromArgb(128, form.GetColor());
                        crack.LineSize = form.GetLineSize();
                        crack.Orientation = form.GetOrientation();
                        crack.Mode = form.GetMode();
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
            string cameraName = this.stage == RCCMStage.RCCM1 ? "wfov 1" : "wfov 2";
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
                if (this.cracks[this.ActiveIndex].Camera == "wfov 1")
                {
                    this.rccm.motors["fine 1 X"].SetPos(m.FineX);
                    this.rccm.motors["fine 1 X"].WaitForEndOfMove();
                    this.rccm.motors["fine 1 Y"].SetPos(m.FineY);
                    this.rccm.motors["fine 1 Y"].WaitForEndOfMove();
                    this.rccm.motors["fine 1 Z"].SetPos(m.FineZ);
                }
                else
                {
                    this.rccm.motors["fine  X"].SetPos(m.FineX);
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

        /// <summary>
        /// Enable buttons for controlling NFOV
        /// </summary>
        private void enableWfovControls()
        {
            btnWfovStart.Enabled = true;
            btnWfovStop.Enabled = true;
            btnWfovSnap.Enabled = true;
            btnWFOVSave.Enabled = true;
            btnWfovRecord.Enabled = true;
            btnWfovProperties.Enabled = true;
            btnFocus.Enabled = true;
            sliderFocus.Enabled = true;
            sliderZoom.Enabled = true;
        }

        /// <summary>
        /// Disable buttons for controlling NFOV
        /// </summary>
        private void disableWfovControls()
        {
            btnWfovStart.Enabled = false;
            btnWfovStop.Enabled = false;
            btnWfovSnap.Enabled = false;
            btnWFOVSave.Enabled = false;
            btnWfovRecord.Enabled = false;
            btnWfovProperties.Enabled = false;
            btnFocus.Enabled = false;
            sliderFocus.Enabled = false;
            sliderZoom.Enabled = false;
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
