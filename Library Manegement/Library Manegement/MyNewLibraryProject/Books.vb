Imports System.Data.SqlClient
Public Class Books
    Dim con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Shivam\Documents\Library Manegement-20211211T065355Z-001\Library Manegement\LibraryVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Display()
        con.open()
        Dim query = "select *from BookTb1"
        Dim adapter As SqlDataAdapter
        Dim cmd = New SqlCommand(query, con)
        adapter = New SqlDataAdapter(cmd)
        Dim builder = New SqlCommandBuilder(adapter)
        Dim ds = New DataSet()
        adapter.Fill(ds)
        BookDGV.DataSource = ds.Tables(0)
        con.close()
    End Sub
    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveBtn.Click
        If BnameTb.Text = "" Or BAuthorTb.Text = "" Or BQuntityTb.Text = "" Or BPriceTb.Text = "" Or BPublisher.Text = "" Then
            MsgBox("Missing Information")
        Else
            con.open()
            Dim query = "insert into BookTb1 values('" & BnameTb.Text & "' ,'" & BAuthorTb.Text & "','" & BPublisher.Text & "'," & BPriceTb.Text & "," & BQuntityTb.Text & ")"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Books Saved")
            con.close()
            Display()
            Reset()
        End If
    End Sub

    Private Sub Books_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
        Reset()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub
    Private Sub Reset()
        BnameTb.Text = ""
        BAuthorTb.Text = ""
        BPriceTb.Text = ""
        BQuntityTb.Text = ""
        BPublisher.Text = ""
    End Sub
    Private Sub ResetBtn_Click(sender As Object, e As EventArgs) Handles ResetBtn.Click
        Reset()
    End Sub
    Dim key = 0

    Private Sub BookDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles BookDGV.CellMouseClick
        Dim row As DataGridViewRow = BookDGV.Rows(e.RowIndex)
        BnameTb.Text = row.Cells(1).Value.ToString
        BAuthorTb.Text = row.Cells(2).Value.ToString
        BPublisher.Text = row.Cells(3).Value.ToString
        BPriceTb.Text = row.Cells(4).Value.ToString
        BQuntityTb.Text = row.Cells(5).Value.ToString
        If BnameTb.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub DeleteBtn_Click(sender As Object, e As EventArgs) Handles DeleteBtn.Click
        If key = 0 Then
            MsgBox("Please select Book")
        Else
            con.open()
            Dim query = "delete from BookTb1 Where BId= " & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Selected Book Deleted")
            con.close()
            Display()
        End If

    End Sub

    Private Sub EditBtn_Click(sender As Object, e As EventArgs) Handles EditBtn.Click
        If BnameTb.Text = "" Or BAuthorTb.Text = "" Or BQuntityTb.Text = "" Or BPriceTb.Text = "" Or BPublisher.Text = "" Then
            MsgBox("Missing Information")
        Else
            con.open()
            Dim query = "update BookTb1 set BName='" & BnameTb.Text & "',BAuthor='" & BAuthorTb.Text & "',BPublisher='" & BPublisher.Text & "',BPrice=" & BPriceTb.Text & ",Bqty=" & BQuntityTb.Text & "where BId = " & key & ""
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            MsgBox("Books Edited")
            con.close()
            Display()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim obj = New MainForm
        obj.Show()
        Me.Hide()

    End Sub
End Class
