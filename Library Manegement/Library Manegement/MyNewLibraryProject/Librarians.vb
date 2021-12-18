Imports System.Data.SqlClient
Public Class Librarians


    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub
    Dim con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shivam\Documents\Library Manegement-20211211T065355Z-001\Library Manegement\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Display()
            con.open()
            Dim query = "select *from LibrarianTb1"
            Dim adapter As SqlDataAdapter
            Dim cmd = New SqlCommand(query, con)
            adapter = New SqlDataAdapter(cmd)
            Dim builder = New SqlCommandBuilder(adapter)
            Dim ds = New DataSet()
            adapter.Fill(ds)
            LibrariansDGV.DataSource = ds.Tables(0)
            con.close()
        End Sub
        Private Sub Reset()
            NameTb.Text = ""
            PhoneTb.Text = ""
            PasswordTb.Text = ""
        End Sub
        Private Sub Libriarians_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Display()
        End Sub

        Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
            If NameTb.Text = "" Or PhoneTb.Text = "" Or PasswordTb.Text = "" Then
                MsgBox("Missing Information")
            Else
                con.open()
                Dim query = "insert into  LibrarianTb1 values('" & NameTb.Text & "' ,'" & PhoneTb.Text & "','" & PasswordTb.Text & "')"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                MsgBox(" Librarian Saved")
                con.close()
                Display()
                Reset()
            End If
        End Sub

        Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
            Reset()
        End Sub
        Dim key = 0
        Private Sub LibrariansDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles LibrariansDGV.CellMouseClick
            Dim row As DataGridViewRow = LibrariansDGV.Rows(e.RowIndex)
            NameTb.Text = row.Cells(1).Value.ToString
            PhoneTb.Text = row.Cells(2).Value.ToString
            PasswordTb.Text = row.Cells(3).Value.ToString
            If NameTb.Text = "" Then
                key = 0
            Else
                key = Convert.ToInt32(row.Cells(0).Value.ToString)
            End If
        End Sub

        Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
            If NameTb.Text = "" Or PhoneTb.Text = "" Or PasswordTb.Text = "" Then
                MsgBox("Missing Information")
            Else
                con.open()
                Dim query = "delete from LibrarianTb1 where LibId =" & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                MsgBox(" Librarian Deleted")
                con.close()
                Display()
                Reset()
            End If
        End Sub

        Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
            If NameTb.Text = "" Or PhoneTb.Text = "" Or PasswordTb.Text = "" Then
                MsgBox("Missing Information")
            Else
                con.open()
            Dim query = "update LibrarianTb1 set LibName= '" & NameTb.Text & "' ,LibPhone='" & PhoneTb.Text & "',LibPass='" & PasswordTb.Text & "' where LibId=" & key & ""
            Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                MsgBox(" Librarian Updated")
                con.close()
                Display()
                Reset()
            End If
        End Sub

        Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        Dim obj = New Login
        obj.Show()
        Me.Hide()

    End Sub
    End Class

