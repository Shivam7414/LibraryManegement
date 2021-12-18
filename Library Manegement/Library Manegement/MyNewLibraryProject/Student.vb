Imports System.Data.SqlClient
Public Class Student


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub
    Dim con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shivam\Documents\Library Manegement-20211211T065355Z-001\Library Manegement\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayStudent()
        con.open()
        Dim query = "select *from StudentTb1"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        StudentDGV.DataSource = ds.Tables(0)
        con.close()
    End Sub
    Private Sub Reset()
        StName.Text = ""
        StDepart.Text = ""
        SemCb.SelectedIndex = 0
        PhoneTb.Text = ""
        key = 0
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If StName.Text = "" Or StDepart.Text = "" Or SemCb.SelectedIndex = -1 Or PhoneTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            con.open()
            Dim query = "insert into StudentTb1 values('" & StName.Text & "' ,'" & StDepart.Text & "'," & SemCb.SelectedItem.ToString & "," & PhoneTb.Text & ")"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Students Saved")
            con.close()
            DisplayStudent()
            Reset()
        End If
    End Sub

    Private Sub Student_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayStudent()
    End Sub

    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub
    Dim key = 0
    Private Sub StudentDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles StudentDGV.CellMouseClick
        Dim row As DataGridViewRow = StudentDGV.Rows(e.RowIndex)
        StName.Text = row.Cells(1).Value.ToString
        StDepart.Text = row.Cells(2).Value.ToString
        SemCb.SelectedItem = row.Cells(3).Value.ToString
        PhoneTb.Text = row.Cells(4).Value.ToString

        If StName.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Select Student")
        Else
            con.open()
            Dim query = "delete from StudentTb1 Where StId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Student Deleted")
            con.close()
            DisplayStudent()
            Reset()
        End If
    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If StName.Text = "" Or StDepart.Text = "" Or SemCb.SelectedIndex = -1 Or PhoneTb.Text = "" Then
            MsgBox("Missing Information")
        Else
            con.open()
            Dim query = "Update StudentTb1 set StName ='" & StName.Text & "' ,StDep ='" & StDepart.Text & "',StSem= " & SemCb.SelectedItem.ToString & ",StPhone = " & PhoneTb.Text & " Where StId=" & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Students Edited")
            con.close()
            DisplayStudent()
            Reset()
        End If
    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        Dim obj = New MainForm
        obj.Show()
        Me.Hide()

    End Sub
End Class

