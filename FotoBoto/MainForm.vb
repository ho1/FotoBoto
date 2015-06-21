''' <summary>
''' The MIT License (MIT)
'''
'''Copyright(c) 2015 Henning Olsson
'''
'''Permission Is hereby granted, free Of charge, to any person obtaining a copy
'''of this software And associated documentation files (the "Software"), to deal
'''in the Software without restriction, including without limitation the rights
'''to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
'''copies of the Software, And to permit persons to whom the Software Is
'''furnished to do so, subject to the following conditions:
'''
'''The above copyright notice And this permission notice shall be included In all
'''copies Or substantial portions of the Software.
'''
'''THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
'''IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
'''FITNESS For A PARTICULAR PURPOSE And NONINFRINGEMENT. In NO Event SHALL THE
'''AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
'''LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
'''OUT Of Or In CONNECTION With THE SOFTWARE Or THE USE Or OTHER DEALINGS In THE
'''SOFTWARE.
'''
''' </summary>


Imports Emgu.CV
Imports Emgu.CV.UI
Imports Emgu.CV.Structure
Imports Emgu.CV.CvEnum

Public Class MainForm

#Region "Init"

    Public Camera As Capture
    Public Frame As Emgu.CV.Image(Of Bgr, Byte)
    Public SaveFrame1 As Emgu.CV.Image(Of Bgr, Byte)
    Public SaveFrame2 As Emgu.CV.Image(Of Bgr, Byte)
    Public SaveFrame3 As Emgu.CV.Image(Of Bgr, Byte)
    Public SaveFrame4 As Emgu.CV.Image(Of Bgr, Byte)
    Public FileNameTick As String ''This is for holding the main photofile names for each run
    Public FourPicElapsed As Double = 0 ''This will containt number of ms since we started the timer which controls to process


    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetupCamera()
        RefreshCamTimer.Interval = 50
        RefreshCamTimer.Start()

    End Sub

    Public Sub SetupCamera()
        Camera = New Capture
        Camera.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 1920)
        Camera.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 1080)
        Me.CameraStatusLabel.Text = "Camera is CONNECTED"

    End Sub

#End Region

#Region "Take 4 Pics Process"

    Public Sub LaunchPhotoProcess()

        Dim tempfileName As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ''The file name will basically be a datestamp
        FileNameTick = tempfileName.Replace(":", "")

        FourPicElapsed = 0
        FourPicTimer.Start()

    End Sub

    Public Sub FourTriggerProcess(ByVal Time As Double)

        Select Case Time
            Case 1000
                SaveFrame(FileNameTick & "_1")
            Case 2000
                SaveFrame(FileNameTick & "_2")
            Case 3000
                SaveFrame(FileNameTick & "_3")
            Case 4000
                SaveFrame(FileNameTick & "_4")

            Case 5000

                FourPicTimer.Stop()
                MergePictures()
                FourPicElapsed = 0
                Me.ProcessLabel.Text = "Process is NOT running"
        End Select

    End Sub

    Private Sub FourPicTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FourPicTimer.Tick

        FourPicElapsed += FourPicTimer.Interval
        FourTriggerProcess(FourPicElapsed)

    End Sub


    Private Sub RefreshCamTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshCamTimer.Tick
        RefreshFrame()
    End Sub

#End Region

#Region "Image Helper Functions"

    Public Sub SaveFrame(ByVal aFilename As String)

        ''Grab the most recent frame and save it
        SaveFrame1 = ProcessFrame()
        SaveFrame1.Save(aFilename & ".jpg")
        My.Computer.Audio.Play("snap1.wav") ''Confirms for user that photo has been taken and they can move to next pose! Source: https://www.freesound.org/people/thecheeseman/sounds/51360/

    End Sub

    Public Overloads Shared Function CropBitmap(ByVal srcBitmap As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap

        ' Create the new bitmap and associated graphics object
        Dim bmp As New Bitmap(cropWidth, cropHeight)

        Dim g As Graphics = Graphics.FromImage(bmp)
        ' Draw the specified section of the source bitmap to the new one
        g.DrawImage(srcBitmap, New Rectangle(0, 0, cropWidth, cropHeight), cropX, cropY, cropWidth, cropHeight, GraphicsUnit.Pixel)
        ' Clean up
        g.Dispose()

        ' Return the bitmap
        Return bmp

    End Function

    Public Sub MergePictures()

        ''Assemble all four shots and add graphic to create a photostrip
        Dim imageFile As New Bitmap(1600, 4800)

        Dim maingraphic As Graphics = Graphics.FromImage(imageFile)
        maingraphic.Clear(Color.White)

        maingraphic.DrawImage(CropBitmap(Image.FromFile(FileNameTick & "_1.jpg"), 240, 0, 1440, 1080), New Point(80, 80))
        maingraphic.DrawImage(CropBitmap(Image.FromFile(FileNameTick & "_2.jpg"), 240, 0, 1440, 1080), New Point(80, 80 + 1080))
        maingraphic.DrawImage(CropBitmap(Image.FromFile(FileNameTick & "_3.jpg"), 240, 0, 1440, 1080), New Point(80, 80 + 1080 * 2))
        maingraphic.DrawImage(CropBitmap(Image.FromFile(FileNameTick & "_4.jpg"), 240, 0, 1440, 1080), New Point(80, 80 + 1080 * 3))
        maingraphic.DrawImage(Image.FromFile("logo.jpg"), New Point(0, 4400))

        imageFile.Save(FileNameTick & "_single.jpg", Imaging.ImageFormat.Jpeg)

        '' Assemble the two strips for print on 4x3 photo paper
        Dim bm As New Bitmap(3200, 4800)
        Dim gr As Graphics = Graphics.FromImage(bm)
        gr.DrawImage(Image.FromFile(FileNameTick & "_single.jpg"), New Point(0, 0))
        gr.DrawImage(Image.FromFile(FileNameTick & "_single.jpg"), New Point(1600, 0))
        bm.Save(FileNameTick & "_double.jpg", Imaging.ImageFormat.Jpeg)

        gr.Dispose()
        bm.Dispose()
        imageFile.Dispose()
        maingraphic.Dispose()

        ''Launch the printing
        PrintDocument1.Print()

        ''This may not be necssary, but seems to keep the memory footprint lower (disposing all the unused bitmaps)
        GC.Collect()
    End Sub
#End Region

#Region "Camera Updates"
    Public Sub RefreshFrame()

        ''Grab the most recent frame using Emgy.CV
        Frame = Camera.QueryFrame()

    End Sub

    Public Function ProcessFrame() As Emgu.CV.Image(Of Bgr, Byte)

        ''Return a copy of the most recent frame for further processing/saving
        Return Frame.Copy()

    End Function
#End Region

#Region "Print Stuff"

    Private Sub PD_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        ''This prints the assembled photo to the default printer. This may require some tinkering depending on your system.
        Dim mainPic As Image = Image.FromFile(FileNameTick & "_double.jpg")
        e.Graphics.DrawImage(mainPic, e.Graphics.VisibleClipBounds)
        e.Graphics.DrawImage(mainPic, e.Graphics.VisibleClipBounds)
        e.HasMorePages = False

    End Sub

#End Region

#Region "USB Button Click"

    Private Sub Button1_MouseDown(ByVal ByValsender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        ''Note that the MainForm window has to be in focus to get this event to trigger
        If e.Button = Windows.Forms.MouseButtons.Right Then

            If FourPicElapsed = 0 Then
                Me.ProcessLabel.Text = "Process is RUNNING"

                My.Computer.Audio.Play("powerup.wav") ''Confirms for user that button has been pressed and process started. Source: http://themushroomkingdom.net/media/smb/wav
                LaunchPhotoProcess()
            End If

        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://www.github.com/ho1/FotoBoto/")
    End Sub

#End Region




End Class
