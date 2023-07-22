namespace TCC1
{
    partial class frmCarregar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCarregar));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.pbFundo = new System.Windows.Forms.PictureBox();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.progressBar = new Bunifu.UI.Winforms.BunifuProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.btnEntrar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFundo)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 25;
            this.bunifuElipse1.TargetControl = this;
            // 
            // pbFundo
            // 
            this.pbFundo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbFundo.Image = ((System.Drawing.Image)(resources.GetObject("pbFundo.Image")));
            this.pbFundo.Location = new System.Drawing.Point(0, 0);
            this.pbFundo.Name = "pbFundo";
            this.pbFundo.Size = new System.Drawing.Size(483, 268);
            this.pbFundo.TabIndex = 0;
            this.pbFundo.TabStop = false;
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.BackColor = System.Drawing.Color.Transparent;
            this.lblPorcentagem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcentagem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(160)))));
            this.lblPorcentagem.Location = new System.Drawing.Point(417, 30);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(44, 16);
            this.lblPorcentagem.TabIndex = 12;
            this.lblPorcentagem.Text = "label3";
            // 
            // progressBar
            // 
            this.progressBar.Animation = 0;
            this.progressBar.AnimationStep = 10;
            this.progressBar.BackColor = System.Drawing.Color.Black;
            this.progressBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("progressBar.BackgroundImage")));
            this.progressBar.BorderColor = System.Drawing.Color.Black;
            this.progressBar.BorderRadius = 20;
            this.progressBar.BorderThickness = 2;
            this.progressBar.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressBar.ForeColor = System.Drawing.Color.White;
            this.progressBar.Location = new System.Drawing.Point(77, 26);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.MaximumValue = 100;
            this.progressBar.MinimumValue = 0;
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressBackColor = System.Drawing.Color.Black;
            this.progressBar.ProgressColorLeft = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(160)))));
            this.progressBar.ProgressColorRight = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.progressBar.Size = new System.Drawing.Size(329, 24);
            this.progressBar.TabIndex = 11;
            this.progressBar.Value = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 120;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 60;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // btnEntrar
            // 
            this.btnEntrar.AllowToggling = false;
            this.btnEntrar.AnimationSpeed = 200;
            this.btnEntrar.AutoGenerateColors = false;
            this.btnEntrar.BackColor = System.Drawing.Color.Transparent;
            this.btnEntrar.BackColor1 = System.Drawing.Color.Black;
            this.btnEntrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEntrar.BackgroundImage")));
            this.btnEntrar.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.btnEntrar.ButtonText = "Entrar";
            this.btnEntrar.ButtonTextMarginLeft = 0;
            this.btnEntrar.ColorContrastOnClick = 45;
            this.btnEntrar.ColorContrastOnHover = 45;
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.btnEntrar.CustomizableEdges = borderEdges1;
            this.btnEntrar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEntrar.DisabledBorderColor = System.Drawing.Color.Empty;
            this.btnEntrar.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnEntrar.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.btnEntrar.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.btnEntrar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnEntrar.IconLeftCursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.IconMarginLeft = 11;
            this.btnEntrar.IconPadding = 10;
            this.btnEntrar.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            this.btnEntrar.IdleBorderRadius = 30;
            this.btnEntrar.IdleBorderThickness = 1;
            this.btnEntrar.IdleFillColor = System.Drawing.Color.Black;
            this.btnEntrar.IdleIconLeftImage = null;
            this.btnEntrar.IdleIconRightImage = null;
            this.btnEntrar.IndicateFocus = false;
            this.btnEntrar.Location = new System.Drawing.Point(152, 142);
            this.btnEntrar.Name = "btnEntrar";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            stateProperties1.BorderRadius = 30;
            stateProperties1.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            stateProperties1.ForeColor = System.Drawing.Color.White;
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnEntrar.onHoverState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            stateProperties2.BorderRadius = 30;
            stateProperties2.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(0)))), ((int)(((byte)(111)))));
            stateProperties2.ForeColor = System.Drawing.Color.White;
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.btnEntrar.OnPressedState = stateProperties2;
            this.btnEntrar.Size = new System.Drawing.Size(179, 35);
            this.btnEntrar.TabIndex = 13;
            this.btnEntrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEntrar.TextMarginLeft = 0;
            this.btnEntrar.UseDefaultRadiusAndThickness = true;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(160)))));
            this.label2.Location = new System.Drawing.Point(156, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Carregado com Sucesso!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(204, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Carregando";
            // 
            // frmCarregar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 268);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pbFundo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCarregar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbFundo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.PictureBox pbFundo;
        private System.Windows.Forms.Label lblPorcentagem;
        private Bunifu.UI.Winforms.BunifuProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer3;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnEntrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}