<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class imgView
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.nowIdxLabel = New System.Windows.Forms.Label()
        Me.InfoLabel = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.imgProgressBar = New System.Windows.Forms.ProgressBar()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnPrev
        '
        Me.btnPrev.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrev.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrev.Location = New System.Drawing.Point(5, 6)
        Me.btnPrev.MaximumSize = New System.Drawing.Size(26, 1000)
        Me.btnPrev.MinimumSize = New System.Drawing.Size(26, 77)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(26, 228)
        Me.btnPrev.TabIndex = 0
        Me.btnPrev.Text = "<"
        Me.btnPrev.UseVisualStyleBackColor = False
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(369, 6)
        Me.btnNext.MaximumSize = New System.Drawing.Size(26, 1000)
        Me.btnNext.MinimumSize = New System.Drawing.Size(26, 77)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(26, 228)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Black
        Me.PictureBox1.Location = New System.Drawing.Point(34, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(332, 228)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Timer1
        '
        '
        'nowIdxLabel
        '
        Me.nowIdxLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nowIdxLabel.AutoSize = True
        Me.nowIdxLabel.BackColor = System.Drawing.Color.Black
        Me.nowIdxLabel.ForeColor = System.Drawing.Color.White
        Me.nowIdxLabel.Location = New System.Drawing.Point(297, 212)
        Me.nowIdxLabel.Name = "nowIdxLabel"
        Me.nowIdxLabel.Size = New System.Drawing.Size(30, 13)
        Me.nowIdxLabel.TabIndex = 6
        Me.nowIdxLabel.Text = "0 / 0"
        Me.nowIdxLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InfoLabel
        '
        Me.InfoLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InfoLabel.BackColor = System.Drawing.Color.Black
        Me.InfoLabel.ForeColor = System.Drawing.Color.White
        Me.InfoLabel.Location = New System.Drawing.Point(37, 62)
        Me.InfoLabel.Name = "InfoLabel"
        Me.InfoLabel.Size = New System.Drawing.Size(326, 118)
        Me.InfoLabel.TabIndex = 7
        Me.InfoLabel.Text = "Display Info"
        Me.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer2
        '
        '
        'imgProgressBar
        '
        Me.imgProgressBar.Location = New System.Drawing.Point(145, 109)
        Me.imgProgressBar.MaximumSize = New System.Drawing.Size(100, 23)
        Me.imgProgressBar.MinimumSize = New System.Drawing.Size(100, 23)
        Me.imgProgressBar.Name = "imgProgressBar"
        Me.imgProgressBar.Size = New System.Drawing.Size(100, 23)
        Me.imgProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.imgProgressBar.TabIndex = 8
        Me.imgProgressBar.Visible = False
        '
        'imgView
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.imgProgressBar)
        Me.Controls.Add(Me.InfoLabel)
        Me.Controls.Add(Me.nowIdxLabel)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnPrev)
        Me.MaximumSize = New System.Drawing.Size(1000, 1000)
        Me.MinimumSize = New System.Drawing.Size(200, 100)
        Me.Name = "imgView"
        Me.Size = New System.Drawing.Size(401, 240)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    

    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents nowIdxLabel As System.Windows.Forms.Label
    Friend WithEvents InfoLabel As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents imgProgressBar As System.Windows.Forms.ProgressBar

End Class
