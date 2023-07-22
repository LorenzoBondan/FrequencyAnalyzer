namespace TCC1
{
    partial class frmSimulador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges7 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties21 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties22 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties23 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties24 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties25 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties stateProperties26 = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges8 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties27 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties28 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges9 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties29 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties30 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimulador));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.plotAudio = new ScottPlot.FormsPlot();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bunifuSeparator2 = new Bunifu.Framework.UI.BunifuSeparator();
            this.btnLimpar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMagnitude = new Bunifu.UI.WinForms.BunifuDropdown();
            this.label12 = new System.Windows.Forms.Label();
            this.rbRuido = new Bunifu.UI.WinForms.BunifuCheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrequencia = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.treeViewSinais = new System.Windows.Forms.TreeView();
            this.btnAdicionarSinal = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnGerarSinal = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.plotFFT = new ScottPlot.FormsPlot();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblFrequenciaScroll = new System.Windows.Forms.Label();
            this.SliderFrequencia = new Bunifu.UI.WinForms.BunifuHSlider();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 10;
            this.bunifuElipse1.TargetControl = this;
            // 
            // plotAudio
            // 
            this.plotAudio.Location = new System.Drawing.Point(213, 40);
            this.plotAudio.Name = "plotAudio";
            this.plotAudio.Size = new System.Drawing.Size(629, 231);
            this.plotAudio.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.bunifuSeparator2);
            this.panel2.Controls.Add(this.btnLimpar);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbMagnitude);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.rbRuido);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.bunifuSeparator1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtFrequencia);
            this.panel2.Controls.Add(this.treeViewSinais);
            this.panel2.Controls.Add(this.btnAdicionarSinal);
            this.panel2.Controls.Add(this.btnGerarSinal);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 653);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(56)))));
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(207, 34);
            this.panel4.TabIndex = 78;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(46, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 18);
            this.label7.TabIndex = 8;
            this.label7.Text = "Simulador";
            // 
            // bunifuSeparator2
            // 
            this.bunifuSeparator2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.bunifuSeparator2.LineThickness = 1;
            this.bunifuSeparator2.Location = new System.Drawing.Point(12, 583);
            this.bunifuSeparator2.Name = "bunifuSeparator2";
            this.bunifuSeparator2.Size = new System.Drawing.Size(179, 16);
            this.bunifuSeparator2.TabIndex = 77;
            this.bunifuSeparator2.Transparency = 255;
            this.bunifuSeparator2.Vertical = false;
            // 
            // btnLimpar
            // 
            this.btnLimpar.AllowToggling = false;
            this.btnLimpar.AnimationSpeed = 200;
            this.btnLimpar.AutoGenerateColors = false;
            this.btnLimpar.BackColor = System.Drawing.Color.Transparent;
            this.btnLimpar.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnLimpar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpar.BackgroundImage")));
            this.btnLimpar.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnLimpar.ButtonText = "Limpar";
            this.btnLimpar.ButtonTextMarginLeft = 0;
            this.btnLimpar.ColorContrastOnClick = 45;
            this.btnLimpar.ColorContrastOnHover = 45;
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges7.BottomLeft = true;
            borderEdges7.BottomRight = true;
            borderEdges7.TopLeft = true;
            borderEdges7.TopRight = true;
            this.btnLimpar.CustomizableEdges = borderEdges7;
            this.btnLimpar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLimpar.DisabledBorderColor = System.Drawing.Color.Empty;
            this.btnLimpar.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnLimpar.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnLimpar.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnLimpar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.ForeColor = System.Drawing.Color.White;
            this.btnLimpar.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.IconMarginLeft = 11;
            this.btnLimpar.IconPadding = 10;
            this.btnLimpar.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(56)))));
            this.btnLimpar.IdleBorderRadius = 30;
            this.btnLimpar.IdleBorderThickness = 1;
            this.btnLimpar.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnLimpar.IdleIconLeftImage = null;
            this.btnLimpar.IdleIconRightImage = null;
            this.btnLimpar.IndicateFocus = false;
            this.btnLimpar.Location = new System.Drawing.Point(12, 605);
            this.btnLimpar.Name = "btnLimpar";
            stateProperties21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties21.BorderRadius = 30;
            stateProperties21.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties21.BorderThickness = 1;
            stateProperties21.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties21.ForeColor = System.Drawing.Color.White;
            stateProperties21.IconLeftImage = null;
            stateProperties21.IconRightImage = null;
            this.btnLimpar.onHoverState = stateProperties21;
            stateProperties22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties22.BorderRadius = 30;
            stateProperties22.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties22.BorderThickness = 1;
            stateProperties22.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties22.ForeColor = System.Drawing.Color.White;
            stateProperties22.IconLeftImage = null;
            stateProperties22.IconRightImage = null;
            this.btnLimpar.OnPressedState = stateProperties22;
            this.btnLimpar.Size = new System.Drawing.Size(179, 32);
            this.btnLimpar.TabIndex = 76;
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpar.TextMarginLeft = 0;
            this.btnLimpar.UseDefaultRadiusAndThickness = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(13, 376);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 17);
            this.label5.TabIndex = 75;
            this.label5.Text = "Ruído";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 74;
            this.label4.Text = "Amplitude";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 73;
            this.label3.Text = "Frequência";
            // 
            // cbMagnitude
            // 
            this.cbMagnitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.cbMagnitude.BorderRadius = 1;
            this.cbMagnitude.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.cbMagnitude.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.cbMagnitude.DisabledColor = System.Drawing.Color.Gray;
            this.cbMagnitude.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbMagnitude.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thick;
            this.cbMagnitude.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMagnitude.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.cbMagnitude.FillDropDown = false;
            this.cbMagnitude.FillIndicator = false;
            this.cbMagnitude.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMagnitude.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMagnitude.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.cbMagnitude.FormattingEnabled = true;
            this.cbMagnitude.Icon = null;
            this.cbMagnitude.IndicatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.cbMagnitude.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.cbMagnitude.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(34)))));
            this.cbMagnitude.ItemBorderColor = System.Drawing.Color.White;
            this.cbMagnitude.ItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.cbMagnitude.ItemHeight = 26;
            this.cbMagnitude.ItemHighLightColor = System.Drawing.Color.Thistle;
            this.cbMagnitude.Items.AddRange(new object[] {
            "0",
            "0.1",
            "0.25",
            "0.5",
            "1",
            "2",
            "5",
            "10",
            "100",
            "1000",
            "10000",
            "100000",
            "1000000"});
            this.cbMagnitude.Location = new System.Drawing.Point(16, 330);
            this.cbMagnitude.Name = "cbMagnitude";
            this.cbMagnitude.Size = new System.Drawing.Size(152, 32);
            this.cbMagnitude.TabIndex = 6;
            this.cbMagnitude.Text = null;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(39, 401);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 16);
            this.label12.TabIndex = 72;
            this.label12.Text = "Inserir Ruído";
            // 
            // rbRuido
            // 
            this.rbRuido.AllowBindingControlAnimation = true;
            this.rbRuido.AllowBindingControlColorChanges = false;
            this.rbRuido.AllowBindingControlLocation = true;
            this.rbRuido.AllowCheckBoxAnimation = false;
            this.rbRuido.AllowCheckmarkAnimation = true;
            this.rbRuido.AllowOnHoverStates = true;
            this.rbRuido.AutoCheck = true;
            this.rbRuido.BackColor = System.Drawing.Color.Transparent;
            this.rbRuido.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rbRuido.BackgroundImage")));
            this.rbRuido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbRuido.BindingControl = null;
            this.rbRuido.BindingControlPosition = Bunifu.UI.WinForms.BunifuCheckBox.BindingControlPositions.Right;
            this.rbRuido.Checked = false;
            this.rbRuido.CheckState = Bunifu.UI.WinForms.BunifuCheckBox.CheckStates.Unchecked;
            this.rbRuido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbRuido.CustomCheckmarkImage = null;
            this.rbRuido.Location = new System.Drawing.Point(12, 399);
            this.rbRuido.MinimumSize = new System.Drawing.Size(17, 17);
            this.rbRuido.Name = "rbRuido";
            this.rbRuido.OnCheck.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.rbRuido.OnCheck.BorderRadius = 1;
            this.rbRuido.OnCheck.BorderThickness = 2;
            this.rbRuido.OnCheck.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.rbRuido.OnCheck.CheckmarkColor = System.Drawing.Color.White;
            this.rbRuido.OnCheck.CheckmarkThickness = 2;
            this.rbRuido.OnDisable.BorderColor = System.Drawing.Color.LightGray;
            this.rbRuido.OnDisable.BorderRadius = 1;
            this.rbRuido.OnDisable.BorderThickness = 2;
            this.rbRuido.OnDisable.CheckBoxColor = System.Drawing.Color.Transparent;
            this.rbRuido.OnDisable.CheckmarkColor = System.Drawing.Color.LightGray;
            this.rbRuido.OnDisable.CheckmarkThickness = 2;
            this.rbRuido.OnHoverChecked.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(131)))), ((int)(((byte)(188)))));
            this.rbRuido.OnHoverChecked.BorderRadius = 1;
            this.rbRuido.OnHoverChecked.BorderThickness = 2;
            this.rbRuido.OnHoverChecked.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(131)))), ((int)(((byte)(188)))));
            this.rbRuido.OnHoverChecked.CheckmarkColor = System.Drawing.Color.White;
            this.rbRuido.OnHoverChecked.CheckmarkThickness = 2;
            this.rbRuido.OnHoverUnchecked.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(131)))), ((int)(((byte)(188)))));
            this.rbRuido.OnHoverUnchecked.BorderRadius = 1;
            this.rbRuido.OnHoverUnchecked.BorderThickness = 2;
            this.rbRuido.OnHoverUnchecked.CheckBoxColor = System.Drawing.Color.Transparent;
            this.rbRuido.OnUncheck.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.rbRuido.OnUncheck.BorderRadius = 1;
            this.rbRuido.OnUncheck.BorderThickness = 2;
            this.rbRuido.OnUncheck.CheckBoxColor = System.Drawing.Color.Transparent;
            this.rbRuido.Size = new System.Drawing.Size(21, 21);
            this.rbRuido.Style = Bunifu.UI.WinForms.BunifuCheckBox.CheckBoxStyles.Round;
            this.rbRuido.TabIndex = 71;
            this.rbRuido.ThreeState = false;
            this.rbRuido.ToolTipText = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Frequências";
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(12, 491);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(179, 16);
            this.bunifuSeparator1.TabIndex = 4;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(174, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hz";
            // 
            // txtFrequencia
            // 
            this.txtFrequencia.AcceptsReturn = false;
            this.txtFrequencia.AcceptsTab = false;
            this.txtFrequencia.AnimationSpeed = 200;
            this.txtFrequencia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtFrequencia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtFrequencia.BackColor = System.Drawing.Color.Transparent;
            this.txtFrequencia.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFrequencia.BackgroundImage")));
            this.txtFrequencia.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.txtFrequencia.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.txtFrequencia.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.txtFrequencia.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.txtFrequencia.BorderRadius = 1;
            this.txtFrequencia.BorderThickness = 1;
            this.txtFrequencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtFrequencia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFrequencia.DefaultFont = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrequencia.DefaultText = "";
            this.txtFrequencia.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.txtFrequencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.txtFrequencia.HideSelection = true;
            this.txtFrequencia.IconLeft = null;
            this.txtFrequencia.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFrequencia.IconPadding = 10;
            this.txtFrequencia.IconRight = null;
            this.txtFrequencia.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFrequencia.Lines = new string[0];
            this.txtFrequencia.Location = new System.Drawing.Point(16, 261);
            this.txtFrequencia.MaxLength = 32767;
            this.txtFrequencia.MinimumSize = new System.Drawing.Size(100, 35);
            this.txtFrequencia.Modified = false;
            this.txtFrequencia.Multiline = false;
            this.txtFrequencia.Name = "txtFrequencia";
            stateProperties23.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties23.FillColor = System.Drawing.Color.Empty;
            stateProperties23.ForeColor = System.Drawing.Color.Empty;
            stateProperties23.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.txtFrequencia.OnActiveState = stateProperties23;
            stateProperties24.BorderColor = System.Drawing.Color.Empty;
            stateProperties24.FillColor = System.Drawing.Color.White;
            stateProperties24.ForeColor = System.Drawing.Color.Empty;
            stateProperties24.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.txtFrequencia.OnDisabledState = stateProperties24;
            stateProperties25.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties25.FillColor = System.Drawing.Color.Empty;
            stateProperties25.ForeColor = System.Drawing.Color.Empty;
            stateProperties25.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.txtFrequencia.OnHoverState = stateProperties25;
            stateProperties26.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            stateProperties26.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            stateProperties26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            stateProperties26.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.txtFrequencia.OnIdleState = stateProperties26;
            this.txtFrequencia.PasswordChar = '\0';
            this.txtFrequencia.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.txtFrequencia.PlaceholderText = "";
            this.txtFrequencia.ReadOnly = false;
            this.txtFrequencia.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFrequencia.SelectedText = "";
            this.txtFrequencia.SelectionLength = 0;
            this.txtFrequencia.SelectionStart = 0;
            this.txtFrequencia.ShortcutsEnabled = true;
            this.txtFrequencia.Size = new System.Drawing.Size(152, 35);
            this.txtFrequencia.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.txtFrequencia.TabIndex = 4;
            this.txtFrequencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFrequencia.TextMarginBottom = 0;
            this.txtFrequencia.TextMarginLeft = 5;
            this.txtFrequencia.TextMarginTop = 0;
            this.txtFrequencia.TextPlaceholder = "";
            this.txtFrequencia.UseSystemPasswordChar = false;
            this.txtFrequencia.WordWrap = true;
            // 
            // treeViewSinais
            // 
            this.treeViewSinais.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.treeViewSinais.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewSinais.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.treeViewSinais.Location = new System.Drawing.Point(12, 71);
            this.treeViewSinais.Name = "treeViewSinais";
            this.treeViewSinais.Size = new System.Drawing.Size(179, 157);
            this.treeViewSinais.TabIndex = 4;
            // 
            // btnAdicionarSinal
            // 
            this.btnAdicionarSinal.AllowToggling = false;
            this.btnAdicionarSinal.AnimationSpeed = 200;
            this.btnAdicionarSinal.AutoGenerateColors = false;
            this.btnAdicionarSinal.BackColor = System.Drawing.Color.Transparent;
            this.btnAdicionarSinal.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnAdicionarSinal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdicionarSinal.BackgroundImage")));
            this.btnAdicionarSinal.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnAdicionarSinal.ButtonText = "Adicionar Sinal";
            this.btnAdicionarSinal.ButtonTextMarginLeft = 0;
            this.btnAdicionarSinal.ColorContrastOnClick = 45;
            this.btnAdicionarSinal.ColorContrastOnHover = 45;
            this.btnAdicionarSinal.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges8.BottomLeft = true;
            borderEdges8.BottomRight = true;
            borderEdges8.TopLeft = true;
            borderEdges8.TopRight = true;
            this.btnAdicionarSinal.CustomizableEdges = borderEdges8;
            this.btnAdicionarSinal.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdicionarSinal.DisabledBorderColor = System.Drawing.Color.Empty;
            this.btnAdicionarSinal.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnAdicionarSinal.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnAdicionarSinal.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnAdicionarSinal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarSinal.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarSinal.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarSinal.IconMarginLeft = 11;
            this.btnAdicionarSinal.IconPadding = 10;
            this.btnAdicionarSinal.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarSinal.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(56)))));
            this.btnAdicionarSinal.IdleBorderRadius = 30;
            this.btnAdicionarSinal.IdleBorderThickness = 1;
            this.btnAdicionarSinal.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnAdicionarSinal.IdleIconLeftImage = null;
            this.btnAdicionarSinal.IdleIconRightImage = null;
            this.btnAdicionarSinal.IndicateFocus = false;
            this.btnAdicionarSinal.Location = new System.Drawing.Point(12, 440);
            this.btnAdicionarSinal.Name = "btnAdicionarSinal";
            stateProperties27.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties27.BorderRadius = 30;
            stateProperties27.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties27.BorderThickness = 1;
            stateProperties27.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties27.ForeColor = System.Drawing.Color.White;
            stateProperties27.IconLeftImage = null;
            stateProperties27.IconRightImage = null;
            this.btnAdicionarSinal.onHoverState = stateProperties27;
            stateProperties28.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties28.BorderRadius = 30;
            stateProperties28.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties28.BorderThickness = 1;
            stateProperties28.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties28.ForeColor = System.Drawing.Color.White;
            stateProperties28.IconLeftImage = null;
            stateProperties28.IconRightImage = null;
            this.btnAdicionarSinal.OnPressedState = stateProperties28;
            this.btnAdicionarSinal.Size = new System.Drawing.Size(179, 32);
            this.btnAdicionarSinal.TabIndex = 5;
            this.btnAdicionarSinal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdicionarSinal.TextMarginLeft = 0;
            this.btnAdicionarSinal.UseDefaultRadiusAndThickness = true;
            this.btnAdicionarSinal.Click += new System.EventHandler(this.btnAdicionarSinal_Click);
            // 
            // btnGerarSinal
            // 
            this.btnGerarSinal.AllowToggling = false;
            this.btnGerarSinal.AnimationSpeed = 200;
            this.btnGerarSinal.AutoGenerateColors = false;
            this.btnGerarSinal.BackColor = System.Drawing.Color.Transparent;
            this.btnGerarSinal.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnGerarSinal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGerarSinal.BackgroundImage")));
            this.btnGerarSinal.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnGerarSinal.ButtonText = "Gerar Sinal";
            this.btnGerarSinal.ButtonTextMarginLeft = 0;
            this.btnGerarSinal.ColorContrastOnClick = 45;
            this.btnGerarSinal.ColorContrastOnHover = 45;
            this.btnGerarSinal.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges9.BottomLeft = true;
            borderEdges9.BottomRight = true;
            borderEdges9.TopLeft = true;
            borderEdges9.TopRight = true;
            this.btnGerarSinal.CustomizableEdges = borderEdges9;
            this.btnGerarSinal.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGerarSinal.DisabledBorderColor = System.Drawing.Color.Empty;
            this.btnGerarSinal.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnGerarSinal.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnGerarSinal.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnGerarSinal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarSinal.ForeColor = System.Drawing.Color.White;
            this.btnGerarSinal.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarSinal.IconMarginLeft = 11;
            this.btnGerarSinal.IconPadding = 10;
            this.btnGerarSinal.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.btnGerarSinal.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(56)))));
            this.btnGerarSinal.IdleBorderRadius = 40;
            this.btnGerarSinal.IdleBorderThickness = 1;
            this.btnGerarSinal.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.btnGerarSinal.IdleIconLeftImage = null;
            this.btnGerarSinal.IdleIconRightImage = null;
            this.btnGerarSinal.IndicateFocus = false;
            this.btnGerarSinal.Location = new System.Drawing.Point(12, 522);
            this.btnGerarSinal.Name = "btnGerarSinal";
            stateProperties29.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties29.BorderRadius = 40;
            stateProperties29.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties29.BorderThickness = 1;
            stateProperties29.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties29.ForeColor = System.Drawing.Color.White;
            stateProperties29.IconLeftImage = null;
            stateProperties29.IconRightImage = null;
            this.btnGerarSinal.onHoverState = stateProperties29;
            stateProperties30.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties30.BorderRadius = 40;
            stateProperties30.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties30.BorderThickness = 1;
            stateProperties30.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties30.ForeColor = System.Drawing.Color.White;
            stateProperties30.IconLeftImage = null;
            stateProperties30.IconRightImage = null;
            this.btnGerarSinal.OnPressedState = stateProperties30;
            this.btnGerarSinal.Size = new System.Drawing.Size(179, 45);
            this.btnGerarSinal.TabIndex = 4;
            this.btnGerarSinal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGerarSinal.TextMarginLeft = 0;
            this.btnGerarSinal.UseDefaultRadiusAndThickness = true;
            this.btnGerarSinal.Click += new System.EventHandler(this.btnGerarSinal_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(27)))), ((int)(((byte)(56)))));
            this.panel1.Controls.Add(this.btnMinimizar);
            this.panel1.Controls.Add(this.btnFechar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(207, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 34);
            this.panel1.TabIndex = 4;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(549, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(39, 34);
            this.btnMinimizar.TabIndex = 6;
            this.btnMinimizar.Text = "_";
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(591, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 34);
            this.btnFechar.TabIndex = 5;
            this.btnFechar.Text = "X";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // plotFFT
            // 
            this.plotFFT.Location = new System.Drawing.Point(207, 270);
            this.plotFFT.Name = "plotFFT";
            this.plotFFT.Size = new System.Drawing.Size(635, 226);
            this.plotFFT.TabIndex = 3;
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.cbLog.Location = new System.Drawing.Point(259, 502);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(151, 20);
            this.cbLog.TabIndex = 7;
            this.cbLog.Text = "Escala Logaritmica";
            this.cbLog.UseVisualStyleBackColor = true;
            this.cbLog.CheckedChanged += new System.EventHandler(this.cbLog_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.DarkGray;
            this.label18.Location = new System.Drawing.Point(500, 534);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(74, 16);
            this.label18.TabIndex = 81;
            this.label18.Text = "Frequência";
            // 
            // lblFrequenciaScroll
            // 
            this.lblFrequenciaScroll.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrequenciaScroll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.lblFrequenciaScroll.Location = new System.Drawing.Point(259, 550);
            this.lblFrequenciaScroll.Name = "lblFrequenciaScroll";
            this.lblFrequenciaScroll.Size = new System.Drawing.Size(556, 30);
            this.lblFrequenciaScroll.TabIndex = 80;
            this.lblFrequenciaScroll.Text = "0 hz";
            this.lblFrequenciaScroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SliderFrequencia
            // 
            this.SliderFrequencia.AllowCursorChanges = true;
            this.SliderFrequencia.AllowHomeEndKeysDetection = false;
            this.SliderFrequencia.AllowIncrementalClickMoves = true;
            this.SliderFrequencia.AllowMouseDownEffects = false;
            this.SliderFrequencia.AllowMouseHoverEffects = false;
            this.SliderFrequencia.AllowScrollingAnimations = true;
            this.SliderFrequencia.AllowScrollKeysDetection = true;
            this.SliderFrequencia.AllowScrollOptionsMenu = true;
            this.SliderFrequencia.AllowShrinkingOnFocusLost = false;
            this.SliderFrequencia.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SliderFrequencia.BackgroundImage")));
            this.SliderFrequencia.BindingContainer = null;
            this.SliderFrequencia.BorderRadius = 2;
            this.SliderFrequencia.BorderThickness = 1;
            this.SliderFrequencia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SliderFrequencia.DrawThickBorder = false;
            this.SliderFrequencia.DurationBeforeShrink = 2000;
            this.SliderFrequencia.ElapsedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.SliderFrequencia.LargeChange = 10;
            this.SliderFrequencia.Location = new System.Drawing.Point(259, 583);
            this.SliderFrequencia.Maximum = 22050;
            this.SliderFrequencia.Minimum = 0;
            this.SliderFrequencia.MinimumSize = new System.Drawing.Size(0, 31);
            this.SliderFrequencia.MinimumThumbLength = 18;
            this.SliderFrequencia.Name = "SliderFrequencia";
            this.SliderFrequencia.OnDisable.ScrollBarBorderColor = System.Drawing.Color.Silver;
            this.SliderFrequencia.OnDisable.ScrollBarColor = System.Drawing.Color.Transparent;
            this.SliderFrequencia.OnDisable.ThumbColor = System.Drawing.Color.Silver;
            this.SliderFrequencia.ScrollBarBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.SliderFrequencia.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.SliderFrequencia.ShrinkSizeLimit = 3;
            this.SliderFrequencia.Size = new System.Drawing.Size(556, 31);
            this.SliderFrequencia.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0)))));
            this.SliderFrequencia.SliderStyle = Bunifu.UI.WinForms.BunifuHSlider.SliderStyles.Thin;
            this.SliderFrequencia.SliderThumbStyle = Utilities.BunifuSlider.BunifuHScrollBar.SliderThumbStyles.Circular;
            this.SliderFrequencia.SmallChange = 1;
            this.SliderFrequencia.TabIndex = 79;
            this.SliderFrequencia.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.SliderFrequencia.ThumbFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.SliderFrequencia.ThumbLength = 18;
            this.SliderFrequencia.ThumbMargin = 1;
            this.SliderFrequencia.ThumbStyle = Bunifu.UI.WinForms.BunifuHSlider.ThumbStyles.Outline;
            this.SliderFrequencia.Value = 0;
            this.SliderFrequencia.Scroll += new System.EventHandler<Utilities.BunifuSlider.BunifuHScrollBar.ScrollEventArgs>(this.SliderFrequencia_Scroll);
            // 
            // frmSimulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(101)))), ((int)(((byte)(101)))));
            this.ClientSize = new System.Drawing.Size(842, 653);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblFrequenciaScroll);
            this.Controls.Add(this.SliderFrequencia);
            this.Controls.Add(this.cbLog);
            this.Controls.Add(this.plotFFT);
            this.Controls.Add(this.plotAudio);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSimulador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulador";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private ScottPlot.FormsPlot plotAudio;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnGerarSinal;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnAdicionarSinal;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox txtFrequencia;
        private System.Windows.Forms.TreeView treeViewSinais;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private Bunifu.UI.WinForms.BunifuCheckBox rbRuido;
        private Bunifu.UI.WinForms.BunifuDropdown cbMagnitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator2;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnLimpar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private ScottPlot.FormsPlot plotFFT;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblFrequenciaScroll;
        private Bunifu.UI.WinForms.BunifuHSlider SliderFrequencia;
    }
}