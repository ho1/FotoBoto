<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.RefreshCamTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FourPicTimer = New System.Windows.Forms.Timer(Me.components)
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.CameraStatusLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProcessLabel = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'RefreshCamTimer
        '
        '
        'FourPicTimer
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDocument1
        '
        '
        'CameraStatusLabel
        '
        Me.CameraStatusLabel.AutoSize = True
        Me.CameraStatusLabel.Location = New System.Drawing.Point(144, 31)
        Me.CameraStatusLabel.Name = "CameraStatusLabel"
        Me.CameraStatusLabel.Size = New System.Drawing.Size(107, 13)
        Me.CameraStatusLabel.TabIndex = 0
        Me.CameraStatusLabel.Text = "Camera is connected"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(104, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(198, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Press right mouse button to start process"
        '
        'ProcessLabel
        '
        Me.ProcessLabel.AutoSize = True
        Me.ProcessLabel.Location = New System.Drawing.Point(144, 173)
        Me.ProcessLabel.Name = "ProcessLabel"
        Me.ProcessLabel.Size = New System.Drawing.Size(119, 13)
        Me.ProcessLabel.TabIndex = 2
        Me.ProcessLabel.Text = "Process is NOT running"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(140, 387)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(162, 13)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "www.github.com/ho1/FotoBoto/"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(419, 409)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.ProcessLabel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CameraStatusLabel)
        Me.Name = "MainForm"
        Me.Text = "FotoBoto - Photobooth software"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageBox1 As Emgu.CV.UI.ImageBox
    Friend WithEvents RefreshCamTimer As System.Windows.Forms.Timer
    Friend WithEvents FourPicTimer As System.Windows.Forms.Timer
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents CameraStatusLabel As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ProcessLabel As Label
    Friend WithEvents LinkLabel1 As LinkLabel
End Class
