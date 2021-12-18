Imports System.Data.SqlClient
Public Class Bookissue
    Dim con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shivam\Documents\Library Manegement-20211211T065355Z-001\Library Manegement\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Display()
            con.open()
            Dim query = "select *from IssueTb1"
            Dim adapter As SqlDataAdapter
            Dim cmd = New SqlCommand(query, con)
            adapter = New SqlDataAdapter(cmd)
            Dim builder = New SqlCommandBuilder(adapter)
            Dim ds = New DataSet()
            adapter.Fill(ds)
            IssueDGV.DataSource = ds.Tables(0)
            con.close()
        End Sub
        Private Sub FillStudent()
            con.open()
            Dim query = "select *from StudentTb1"
            Dim cmd As New SqlCommand(query, con)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim Tb1 As New DataTable()
            adapter.Fill(Tb1)
            StIdCb.DataSource = Tb1
            StIdCb.DisplayMember = "StId"
            StIdCb.ValueMember = "StId"
            con.close()
        End Sub
        Private Sub GetStudentName()
            con.open()
            Dim query = "select *from StudentTb1 where StId=" & StIdCb.SelectedValue.ToString() & ""
            Dim cmd = New SqlCommand(query, con)
            Dim dt As New DataTable
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                StNameTb.Text = "" + reader(1).ToString
            End While
            con.close()
        End Sub
        Private Sub FillBook()
            con.open()
            Dim query = "select *from BookTb1"
            Dim cmd As New SqlCommand(query, con)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim Tb1 As New DataTable()
            adapter.Fill(Tb1)
            BIdCb.DataSource = Tb1
            BIdCb.DisplayMember = "BId"
            BIdCb.ValueMember = "BId"
            con.close()
        End Sub
        Private Sub GetBookName()
            con.open()
            Dim query = "select *from BookTb1 where BId=" & BIdCb.SelectedValue.ToString() & ""
            Dim cmd = New SqlCommand(query, con)
            Dim dt As New DataTable
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader
            While reader.Read
                BNameTb.Text = "" + reader(1).ToString
            End While
            con.close()
        End Sub
        Private Sub Bookissue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            FillStudent()
            FillBook()
            Display()
        End Sub
        Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
            Application.Exit()
        End Sub

        Private Sub StIdCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles StIdCb.SelectionChangeCommitted
            GetStudentName()
        End Sub

        Private Sub BIdCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles BIdCb.SelectionChangeCommitted
            GetBookName()
        End Sub
        Private Sub Reset()
            BNameTb.Text = ""
            StNameTb.Text = ""
            StIdCb.SelectedIndex = -1
            BIdCb.SelectedIndex = -1
        End Sub
        Dim Num = 0
        Private Sub CountBook()
            Try
                con.open()
                Dim query = "select Count(*) from IssueTb1 where StId = " & StIdCb.SelectedValue.ToString() & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                Num = cmd.ExecuteScalar
                con.close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub
        Private Sub SubmitBtn_Click(sender As Object, e As EventArgs) Handles SubmitBtn.Click
            CountBook()
            If BNameTb.Text = "" Or StNameTb.Text = "" Or StIdCb.SelectedIndex = -1 Or BIdCb.SelectedIndex = -1 Then
                MsgBox("Missing Information")
            ElseIf Num = 2 Then
                MsgBox("No More then 5 books Issued")

            Else
                Try
                    con.open()
                    Dim query = "insert into IssueTb1 values(" & StIdCb.SelectedValue.ToString & ",'" & StNameTb.Text & "'," & BIdCb.SelectedValue.ToString & ",'" & BNameTb.Text & "','" & IssueDate.Value.ToLongDateString & "')"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(query, con)
                    cmd.ExecuteNonQuery()
                    MsgBox("Book Issued")
                    con.close()
                    Display()
                    Reset()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End Sub

        Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
            Dim obj = New MainForm
            obj.Show()
            Me.Hide()
        End Sub

        Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
            Reset()
        End Sub
        Dim key = 0
    Private Sub IssueDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles IssueDGV.CellMouseClick
        Dim row As DataGridViewRow = IssueDGV.Rows(e.RowIndex)
        StIdCb.SelectedItem = row.Cells(1).Value.ToString
        StNameTb.Text = row.Cells(2).Value.ToString
        BIdCb.SelectedItem = row.Cells(3).Value.ToString
        BNameTb.Text = row.Cells(4).Value.ToString
        IssueDate.Text = row.Cells(5).Value.ToString
        If StNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub
    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
            If BNameTb.Text = "" Or StNameTb.Text = "" Or StIdCb.SelectedIndex = -1 Or BIdCb.SelectedIndex = -1 Then
                MsgBox("Missing Information")
            Else
                con.open()
                Dim query = "update IssueTb1 set StId=" & StIdCb.SelectedValue.ToString & ",StName='" & StNameTb.Text & "',BookId=" & BIdCb.SelectedValue.ToString & ",BookName='" & BNameTb.Text & "',IssueDate='" & IssueDate.Value.ToLongDateString & "'where INum = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                MsgBox("Issue Edited")
                con.close()
                Display()
                Reset()
            End If
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            If key = 0 Then
                MsgBox("Select Item")
            Else
                con.open()
                Dim query = "delete from IssueTb1 Where INum=" & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, con)
                cmd.ExecuteNonQuery()
                MsgBox("Student Deleted")
                con.close()
                Display()
                Reset()
            End If
        End Sub
    End Class
