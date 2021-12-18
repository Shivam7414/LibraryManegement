Imports System.Data.SqlClient

Public Class BReturn
    Dim con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shivam\Documents\Library Manegement-20211211T065355Z-001\Library Manegement\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub DisplayBook()
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
    Private Sub ReturnBook()
        con.open()
        Dim query = "select *from ReturnTb1"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        BookReturnDGV.DataSource = ds.Tables(0)
        con.close()
    End Sub
    Private Sub Reset()
        StIdCb.SelectedIndex = -1
        BIdCb.SelectedIndex = -1
        StNameTb.Text = ""
        BNameTb.Text = ""
        FineTb.Text = ""
        key = 0
    End Sub
    Private Sub RemoveFromIssue()
        Try
            con.open()
            Dim query = "delete from IssueTb1 where INum = " & key & " "
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Issued Removed")
            con.close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If StIdCb.Text = "" Or BIdCb.Text = "" Or StNameTb.Text = "" Or BNameTb.Text = "" Or FineTb.Text = "" Then
            MsgBox("Missing Information")
        End If
        con.open()
        Dim query = "insert into ReturnTb1 values(" & StIdCb.Text & ",'" & StNameTb.Text & "'," & BIdCb.Text & ",'" & BNameTb.Text & "','" & IssueDate.Value.ToLongDateString & "','" & ReturnDate.Value.ToLongDateString & "'," & Fine.ToString & ")"
        Dim cmd As SqlCommand
        cmd = New SqlCommand(query, con)
        cmd.ExecuteNonQuery()
        MsgBox("Book Returned")
        con.close()
        RemoveFromIssue()
        ReturnBook()
        DisplayBook()
        Reset()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub

    Private Sub BReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayBook()
        ReturnBook()
    End Sub
    Dim key = 0
    Private Sub IssueDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles IssueDGV.CellMouseClick
        Dim row As DataGridViewRow = IssueDGV.Rows(e.RowIndex)
        StIdCb.Text = row.Cells(1).Value.ToString
        StNameTb.Text = row.Cells(2).Value.ToString
        BIdCb.Text = row.Cells(3).Value.ToString
        BNameTb.Text = row.Cells(4).Value.ToString
        IssueDate.Text = row.Cells(5).Value.ToString
        If StNameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub
    Dim Fine As Integer
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Dif As TimeSpan
        Dif = ReturnDate.Value.Date - IssueDate.Value.Date
        Dim Days = Dif.Days
        If Days <= 15 Then
            Fine = 0
            FineTb.Text = "No Fine"
        Else
            Fine = (Days - 15) * 50
            FineTb.Text = "RS" + Convert.ToString(Fine)

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Reset()
    End Sub

    Private Sub BNameTb_TextChanged(sender As Object, e As EventArgs) Handles BNameTb.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim obj = New MainForm
        obj.Show()
        Me.Hide()

    End Sub
End Class