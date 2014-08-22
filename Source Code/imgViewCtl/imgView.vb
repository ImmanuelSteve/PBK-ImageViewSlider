Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net

Public Enum sourceFile
    Local
    Online
End Enum

Public Enum animationSlideShow
    DefaultAnimation
    FadeOut
End Enum


<Serializable()> _
Public Class imgView
#Region "DATA MEMBERS / VARIABLES"
    'variabel tambahan
    Private slide_interval As Integer 'interval pergantian gambar
    Private slide_enable As Boolean ' apakah slide show atau manual
    Private auto_cycle = False
    Private index_now As Integer ' index arrlist gambar skrng yg ditampilkan
    'gambar/gambar-gambar disimpan, example "F:\gambarku\" jangan lupa '\'
    'arraylist yang digunakan untuk menyimpan daftar nama file dalam 1 folder

    Private imageW As Integer
    Private imageH As Integer

    Private fn_arr_list As New ArrayList()
    Private ms_arr_list As New ArrayList()
    Private err_arr_list As New ArrayList()

    Private WithEvents client As New WebClient()
    'pembaca directory/folder
    Private di As IO.DirectoryInfo
    Private diar1 As IO.FileInfo() ' array dari FileInfo
    Private dra As IO.FileInfo

    Private effectvalue As Integer = 0 'effect utk fadeOut
    Private tampImage As Image 'tampungan Image 

#End Region

#Region "PROPERTIES"
    'property yang mengatur slide show/ tidak
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("Enable slide show")> _
    Public Property EnableSlideShow() As Boolean
        Get
            Return slide_enable
        End Get
        Set(ByVal value As Boolean)
            If (slide_interval < 1 And value) Then
                MessageBox.Show("Set slide interval to more than 0 before set EnableSlideShow to True", "Property Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                slide_enable = value
                Timer1.Enabled = slide_enable
                If animationslide = ImageViewer.animationSlideShow.FadeOut Then
                    showImage(index_now)
                Else
                    Timer2.Stop()
                End If
            End If
        End Set
    End Property
    'tiap berapa detik slide show
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("Interval of image slide show in msecond. Default 1000msecond for Fade Out Efect")> _
    Public Property SlideInterval() As Integer
        Get
            Return slide_interval
        End Get
        Set(ByVal value As Integer)
            If animationslide = ImageViewer.animationSlideShow.DefaultAnimation Then
                slide_interval = value
                Timer1.Interval = slide_interval
            End If
        End Set
    End Property
    'autocycle berarti kalau image sudah sampai paling akhir dan di next akan kembali ke awal
    <Description("Cycle image view, if True, back to first after last image")>
    Public Property AutoCycle() As Boolean
        Get
            Return auto_cycle
        End Get
        Set(ByVal value As Boolean)
            auto_cycle = value
            setCycle(auto_cycle)
        End Set
    End Property

    Private animationslide As animationSlideShow = ImageViewer.animationSlideShow.DefaultAnimation
    <Description("Animation Slide Show")> _
    Public Property AnimationSlideShow() As animationSlideShow
        Get
            Return animationslide
        End Get
        Set(ByVal value As animationSlideShow)
            'value dlm bntk int
            animationslide = value
            Timer2.Enabled = False
            If value = ImageViewer.animationSlideShow.FadeOut Then
                Timer2.Enabled = True
                Timer1.Interval = 1000
                slide_interval = 1000
                If slide_enable Then
                    Timer2.Start()
                End If
            End If
        End Set
    End Property

    'button visibility
    Private buttonVis As Boolean = True
    <Description("Change Next/Prev button visibility")>
    Public Property ButtonVisible() As Boolean
        Get
            Return buttonVis
        End Get
        Set(ByVal value As Boolean)
            If Not value Then
                btnNext.Hide()
                btnPrev.Hide()
            Else
                btnNext.Show()
                btnPrev.Show()
            End If

            buttonVis = value
        End Set
    End Property


#End Region

#Region "METHODS"
    Public Function getNowImageLocation() As String
        Return fn_arr_list.Item(index_now).ToString
    End Function

    Public Sub clearImageList()
        ms_arr_list.Clear()
    End Sub

    Public Sub addLocalImageFolder(ByVal path As String)
        Try
            di = New IO.DirectoryInfo(path)
            diar1 = di.GetFiles.Where(Function(fi) fi.Extension = ".png" OrElse
                                                                    fi.Extension = ".jpg" OrElse
                                                                    fi.Extension = ".gif" OrElse
                                                                    fi.Extension = ".jpeg").ToArray
            If Not (path.EndsWith("\") Or path.EndsWith("/")) Then
                path = path + "/"
            End If
            For Each Me.dra In diar1
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(path + dra.ToString))
                bData = br.ReadBytes(br.BaseStream.Length)
                ms_arr_list.Add(New MemoryStream(bData, 0, bData.Length)) ' masukkan nama-nama file kedalam arrlist
                fn_arr_list.Add(path + dra.ToString)
                err_arr_list.Add("")
            Next

            showImage(index_now)
        Catch ex As Exception
            err_arr_list.Add(ex.Message.ToString())
        End Try

    End Sub

    Public Sub addImageURL(ByVal path As String)
        Try
            'client.DownloadDataAsync(New Uri(path))
            ms_arr_list.Add(New  _
            MemoryStream(client.DownloadData(path)))
            showImage(index_now)
            client.Dispose()
            err_arr_list.Add("")
            fn_arr_list.Add(path)
        Catch ex As Exception
            err_arr_list.Add(ex.Message.ToString())
        End Try

    End Sub

    Public Shared Function ChangeOpacity(img As Image, width As Integer, height As Integer, opacityvalue As Single) As Bitmap
        Dim bmp As New Bitmap(width, height)
        Dim graphics__1 As Graphics = Graphics.FromImage(bmp)
        Dim colormatrix As New ColorMatrix()
        colormatrix.Matrix33 = opacityvalue
        Dim imgAttribute As New ImageAttributes()
        imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
        graphics__1.DrawImage(img, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, _
            GraphicsUnit.Pixel, imgAttribute)
        graphics__1.Dispose()
        Return bmp
    End Function

    Private Sub showImage(index As Integer)
        
        If InfoLabel.Visible And ms_arr_list.Count <> 0 Then
            InfoLabel.Visible = False
            PictureBox1.Enabled = True
        End If
        Try

            '<> tidak sama dgn
            If (ms_arr_list.Count <> 0) Then
                If Not (err_arr_list.Item(index) = "") Then
                    showInfo(err_arr_list.Item(index))
                    Return
                End If
                Dim bm_source As Bitmap = Bitmap.FromStream(ms_arr_list.Item(index))

                ' scaling untuk gambar
                Dim w As Integer = bm_source.Width
                Dim h As Integer = bm_source.Height

                imageW = bm_source.Width
                imageH = bm_source.Height

                If bm_source.Width > PictureBox1.Width Or bm_source.Height > PictureBox1.Height Then
                    Dim scaleX, scaleY As Double
                    scaleX = PictureBox1.Width / bm_source.Width
                    scaleY = PictureBox1.Height / bm_source.Height
                    If scaleX < scaleY Then
                        h = bm_source.Height * scaleX
                        w = bm_source.Width * scaleX
                    Else
                        h = bm_source.Height * scaleY
                        w = bm_source.Width * scaleY
                    End If
                    If h > imageH Or w > imageW Then
                        h = imageH
                        w = imageW
                    End If
                End If
                Dim bm_dest As New Bitmap( _
                    CInt(w), _
                    CInt(h))

                Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

                gr_dest.DrawImage(bm_source, 0, 0, _
                    bm_dest.Width + 1, _
                    bm_dest.Height + 1)
                PictureBox1.Image = bm_dest
                tampImage = bm_dest
                'perubahan #1
                If animationslide = ImageViewer.animationSlideShow.DefaultAnimation Then
                    Timer2.Stop()
                    Timer1.Interval = slide_interval
                    If Timer1.Enabled Then
                        Timer1.Start()
                    End If
                ElseIf animationslide = ImageViewer.animationSlideShow.FadeOut Then
                    effectvalue = 300
                    Timer1.Interval = Timer2.Interval * (effectvalue / 20)
                    Timer2.Start() ' perubahan
                End If
                'perubahan #1
            End If
            If ms_arr_list.Count <> 0 Then
                nowIdxLabel.Text = (index_now + 1).ToString + " / " + ms_arr_list.Count.ToString
            End If
        Catch ex As Exception
            showInfo(ex.Message)
        End Try
    End Sub

    'jika komponen di resize di form aplikasi(koordinat posisi mengikuti)
    Private Sub resizeInsidePictureBox1()
        Dim x = PictureBox1.Location.X + PictureBox1.Size.Width / 2 - imgProgressBar.Size.Width / 2
        Dim y = PictureBox1.Location.Y + PictureBox1.Size.Height / 2 - imgProgressBar.Size.Height / 2
        imgProgressBar.Location = New Point(x, y)
        x = PictureBox1.Location.X + (PictureBox1.Size.Width - (nowIdxLabel.Size.Width)) - 3
        y = PictureBox1.Location.Y + (PictureBox1.Size.Height - (nowIdxLabel.Size.Height)) - 3
        nowIdxLabel.Location = New Point(x, y)
    End Sub

    Private Sub showNextImage()
        If (ms_arr_list.Count <> 0) Then
            'fungsi modulus utk menghandle ketika posisi index = length-1
            index_now = (index_now + 1) Mod ms_arr_list.Count
            If (Not auto_cycle And index_now = ms_arr_list.Count - 1) Then btnNext.Enabled = False

            If (auto_cycle <> True And index_now > 0) Then
                btnPrev.Enabled = True
            End If
            If (index_now < ms_arr_list.Count) Then
                If Timer1.Enabled Then
                    Timer1.Stop()
                    showImage(index_now)
                    Timer1.Start()
                Else
                    showImage(index_now)
                End If
            End If
            setCycle(auto_cycle)
        End If
    End Sub

    Private Sub showPrevImage()
        If (ms_arr_list.Count <> 0) Then
            index_now -= 1
            If AutoCycle Then
                If index_now < 0 Then
                    index_now = ms_arr_list.Count - 1
                End If
            Else
                If index_now = 0 Then
                    btnPrev.Enabled = False
                End If
            End If
            If slide_enable And Timer1.Enabled = False Then
                Timer1.Enabled = True
            End If
            If Timer1.Enabled Then
                Timer1.Stop()
                showImage(index_now)
                Timer1.Start()
            Else
                showImage(index_now)
            End If
            setCycle(auto_cycle)
        End If
    End Sub

    'display error info
    Private Sub showInfo(message As String)
        InfoLabel.Visible = True
        InfoLabel.Text = message
        PictureBox1.Enabled = False
    End Sub

    'button function - prev
    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        showPrevImage()
    End Sub
    'button function - next
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        showNextImage()
    End Sub

    'time tick
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ms_arr_list.Count = 0 Or (Not auto_cycle And index_now = ms_arr_list.Count - 1) Then
            Timer1.Stop()
            Return
        End If
        If animationslide = ImageViewer.animationSlideShow.DefaultAnimation Then
            showNextImage()
        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If ms_arr_list.Count = 0 Or (Not auto_cycle And index_now = ms_arr_list.Count - 1) Then
            Timer2.Stop()
            Return
        Else
            If Timer1.Enabled = False Then
                PictureBox1.Image = tampImage
                Timer2.Enabled = slide_enable
            Else
                If effectvalue > 0 Then
                    effectvalue = effectvalue - 20
                Else
                    Timer2.Stop()
                    showNextImage()
                End If
                Dim o As Single = effectvalue
                If effectvalue < 100 Then
                    PictureBox1.Image = ChangeOpacity(PictureBox1.Image, PictureBox1.Image.Width, PictureBox1.Image.Height, o / 100)
                End If
            End If
        End If
    End Sub
    Private Sub setCycle(val As Boolean)
        If (val = False) Then 'no cycle
            If (ms_arr_list.Count = 1) Then 'only 1 pic
                btnNext.Enabled = False
                btnPrev.Enabled = False
            Else 'more than 1 pic
                If index_now = 0 Then ' kalau ada di paling awal prev didisable
                    btnNext.Enabled = True
                    btnPrev.Enabled = False
                ElseIf index_now = ms_arr_list.Count - 1 Then ' kalau ada di tengah
                    btnNext.Enabled = False
                    btnPrev.Enabled = True
                Else ' kalau ada di paling akhir
                    btnPrev.Enabled = True
                    btnNext.Enabled = True
                End If
            End If

        Else 'cycle
            If (ms_arr_list.Count = 1 Or ms_arr_list.Count = 0) Then 'only 1 pic
                btnNext.Enabled = False
                btnPrev.Enabled = False
            Else 'more than 1 pic
                btnNext.Enabled = True
                btnPrev.Enabled = True
                If slide_enable And index_now = ms_arr_list.Count - 1 Then Timer1.Start()
            End If
        End If
    End Sub

    Private Sub imgView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        resizeInsidePictureBox1()
    End Sub

    'resizing
    Private Sub pictureBox1_onResize(sender As Object, e As EventArgs) Handles PictureBox1.Resize
        resizeInsidePictureBox1()
        'showImage(index_now)
    End Sub
    Private Sub pictureBox1_sizeChanged(sender As Object, e As EventArgs) Handles PictureBox1.SizeChanged
        'resizeInsidePictureBox1()
        showImage(index_now)
    End Sub

    Private Sub nowIdxLabel_SizeChanged(sender As Object, e As EventArgs) Handles nowIdxLabel.SizeChanged
        resizeInsidePictureBox1()
    End Sub
#End Region

#Region "INITIALIZATION"
    'initialization goes here
    Public Sub New()
        InitializeComponent()

        slide_interval = 1000 'interval pergantian gambar
        slide_enable = False ' apakah slide show atau manual
        auto_cycle = False
        index_now = 0
        Timer2.Interval = 250
        'addLocalImageFolder("F:\images\iu")

        InfoLabel.Visible = False
        If (slide_interval > 0) Then
            Timer1.Interval = slide_interval
            Timer1.Enabled = slide_enable
        Else
            Timer1.Enabled = False
        End If
        setCycle(auto_cycle)
        If ms_arr_list.Count = 0 Then showInfo("No image to view")

    End Sub
#End Region

End Class
