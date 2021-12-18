Imports System.Data.SqlClient

Public Class Login
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Application.Exit()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim obj = New AdminForm
        obj.Show()
        Me.Hide()
    End Sub
    Dim con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shivam\Documents\Library Manegement-20211211T065355Z-001\Library Manegement\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If UName.Text = "" Then
            MsgBox("Enter Librarian Name")
        ElseIf PassTb.Text = "" Then
            MsgBox("Enter Password")
        Else
            con.open()
            Dim query = "select *from LibrarianTb1 where LibName = '" & UName.Text & "' and LibPass='" & PassTb.Text & "'"
            Dim cmd = New SqlCommand(query, con)
            Dim da = New SqlDataAdapter(cmd)
            Dim ds = New DataSet()
            da.Fill(ds)
            Dim a As Integer
            a = ds.Tables(0).Rows.Count
            If a = 0 Then
                MsgBox("wrong UserName and Password")
            Else
                Dim obj = New MainForm
                obj.Show()
                Me.Hide()
            End If
            con.close()
            End If

    End Sub
End Class